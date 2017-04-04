using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

	[SerializeField] float shotCoolDown = 0.3f;
	[SerializeField] Transform firePosition;
	[SerializeField] ShotEffectManager shotEffects;

	float elaspedTime;
	bool canShot;

	void Start () {
		shotEffects.Initialize ();
		if (isLocalPlayer) 
		{
			canShot = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!canShot)
			return;

		elaspedTime += Time.deltaTime;

		if (Input.GetButtonDown ("Fire1") && elaspedTime > shotCoolDown) 
		{
			elaspedTime = 0;
			CmdFireShot (firePosition.position, firePosition.forward);
		}

	}
	[Command]
	void CmdFireShot(Vector3 origin, Vector3 direction)
	{
		RaycastHit hit;

		Ray ray = new Ray (origin, direction);
		Debug.DrawRay (ray.origin,ray.direction *3f,Color.red, 1f);

		bool result = Physics.Raycast (ray, out hit, 50f); 	

		if (result) 
		{
			//health stuff
			PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth> ();

			if (enemy != null)
				enemy.TakeDamage ();
		}
		RpcProcessShotEffect (result, hit.point);
	}
	[ClientRpc]
	void RpcProcessShotEffect( bool playImpact, Vector3 point)
	{
		shotEffects.PlayShotEffects ();

		if (playImpact) 
		{
			shotEffects.PlayImpactEffects (point);
		}
	}
}
