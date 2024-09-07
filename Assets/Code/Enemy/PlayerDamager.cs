using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager
	:
	MonoBehaviour
{
	void OnTriggerEnter2D( Collider2D coll )
	{
		coll.GetComponent<HealthBar>()?.Hurt( 1.0f );
	}
}
