using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoxBullet
	:
	MonoBehaviour
{
	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.tag == "Player" ||
			coll.gameObject.tag == "SpaceKitten" )
		{
			coll.gameObject.GetComponent<HealthBar>()
				.Hurt( 1 );
		}
		Destroy( gameObject );
	}
}
