using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityWall
	:
	MonoBehaviour
{
	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.gameObject.tag == "Player" )
		{
			if( coll.gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude >
				Mathf.Pow( breakForce,2 ) )
			{
				PartHand.Get().SpawnParts( transform.position,PartHand.PartType.Explode );
				Destroy( gameObject );
			}
		}
	}

	[SerializeField] float breakForce = 12.0f;
}
