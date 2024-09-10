using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquisherYeeter
	:
	MonoBehaviour
{
	void OnCollisionEnter2D( Collision2D coll )
	{
		TryYeet( coll.gameObject );
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		TryYeet( coll.gameObject );
		
		coll.GetComponent<HealthBar>()?.Hurt( 1.0f );
	}

	void OnTriggerStay2D( Collider2D coll )
	{
		TryYeet( coll.gameObject );
		
		coll.GetComponent<HealthBar>()?.Hurt( 1.0f );
	}

	void TryYeet( GameObject obj )
	{
		if( canYeet )
		{
			var body = obj.GetComponent<Rigidbody2D>();
			if( body != null )
			{
				// body.AddForce( transform.up * yeetForce,ForceMode2D.Impulse );
				body.velocity += ( Vector2 )transform.up * yeetForce;
			}
		}
	}

	public void ToggleCanYeet( bool canYeet )
	{
		this.canYeet = canYeet;
	}

	[SerializeField] float yeetForce = 50.0f;

	bool canYeet = false;
}
