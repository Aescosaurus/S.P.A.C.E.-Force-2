using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquisherDamager
	:
	MonoBehaviour
{
	void OnTriggerEnter2D( Collider2D coll )
	{
		coll.GetComponent<HealthBar>()?.Hurt( 9999.0f );
	}
}
