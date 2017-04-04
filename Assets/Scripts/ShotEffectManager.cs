using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEffectManager : MonoBehaviour {

	[SerializeField] ParticleSystem muzzleFlash;
	[SerializeField] AudioSource gunAudio;
	[SerializeField] GameObject impactPrefab;

	ParticleSystem impactEffect;

	public void Initialize()
	{
		impactEffect = Instantiate (impactPrefab).GetComponent<ParticleSystem> ();
	}
	public void PlayShotEffects()
	{
		muzzleFlash.Stop (true);
		muzzleFlash.Play (true);
		gunAudio.Stop ();
		gunAudio.Play ();
	}
	public void PlayImpactEffects(Vector3 impactPosition)
	{
		impactEffect.transform.position = impactPosition;
		impactEffect.Stop ();
		impactEffect.Play ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
