using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool>{}

public class Player : NetworkBehaviour {

	[SerializeField] ToggleEvent onToggleShared;
	[SerializeField] ToggleEvent onToggleLocal;
	[SerializeField] ToggleEvent onToggleRemote;

	[SerializeField] float respawnTime = 2.0f;
	GameObject mainCamera;

	void Start()
	{
		mainCamera = Camera.main.gameObject;
		EnablePlayer ();
	}
	void DisablePlayer()
	{
		if (isLocalPlayer)
			mainCamera.SetActive (true);
		onToggleShared.Invoke (false);
		if (isLocalPlayer)
			onToggleLocal.Invoke (false);
		else
			onToggleRemote.Invoke (false);
	}
	void EnablePlayer()
	{
		if (isLocalPlayer)
			mainCamera.SetActive (false);
		onToggleShared.Invoke (true);
		if (isLocalPlayer)
			onToggleLocal.Invoke (true);
		else
			onToggleRemote.Invoke (true);
	}

	public void Die()
	{
		DisablePlayer ();

		Invoke ("Respawn",respawnTime);
	}
	void Respawn()
	{
		EnablePlayer ();

		if (isLocalPlayer) 
		{
			Transform spawn = NetworkManager.singleton.GetStartPosition ();
			transform.position = spawn.position;
			transform.rotation = spawn.rotation;
		}
	}
}
