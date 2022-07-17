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
			
			var trail = Instantiate( trailPrefab,coll.transform );
			Destroy( trail,1.2f );
		}
	}

	[SerializeField] float pushForce = 2.0f;
	[SerializeField] float velTamper = 0.7f;

	[SerializeField] GameObject trailPrefab = null;
}
