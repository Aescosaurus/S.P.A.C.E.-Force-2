using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor
	:
	MonoBehaviour
{
	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.tag == "Player" )
		{
			if( coll.GetComponent<PlayerHeldItems>().TryConsumeKey() )
			{
				PartHand.Get().SpawnParts( transform.position );
				Destroy( gameObject );
			}
		}
	}
}
