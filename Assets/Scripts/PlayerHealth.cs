using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

	[SerializeField] int maxHealth = 3;
	Player player;
	int health;

	void Awake()
	{
		player = GetComponent<Player> ();
	}

	[ServerCallback]
	void OnEnable()
	{
		health = maxHealth;
	}

	[Server]
	public bool TakeDamage()
	{
		bool died = false;
		if (health <= 0)
			return died; 

		health--;
		died = health <= 0;

		RpcTakeDamage (died);

		return died;
	}
	[ClientRpc]
	void RpcTakeDamage(bool died)
	{
		if (died)
			player.Die ();
	}
}
