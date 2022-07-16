using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDamage
	:
	MonoBehaviour
{
	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.tag == "SpaceFox" )
		{
			GetComponent<HealthBar>().Hurt( 1.0f );
		}
	}
}
