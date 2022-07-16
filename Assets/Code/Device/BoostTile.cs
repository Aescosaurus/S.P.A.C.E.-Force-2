using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTile
	:
	MonoBehaviour
{
	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.tag == "Player" )
		{
			var playerBody = coll.GetComponent<Rigidbody2D>();
			playerBody.velocity *= velTamper;
			playerBody.AddForce( transform.up * pushForce,ForceMode2D.Impulse );
			// todo: particle trail and/or actual trail on player
		}
	}

	[SerializeField] float pushForce = 2.0f;
	[SerializeField] float velTamper = 0.7f;
}
