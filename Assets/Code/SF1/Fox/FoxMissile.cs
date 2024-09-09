using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoxMissile
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );

		var rand = new Vector2( Random.Range( -1.0f,1.0f ),
			Random.Range( -1.0f,1.0f ) );
		rand.Normalize();
		body.AddForce( rand * deviationSpeed,ForceMode2D.Impulse );
	}

	void Update()
	{
		if( lockOn.Update( Time.deltaTime ) && !targeted )
		{
			targeted = true;
			body.gravityScale = 0.0f;
			body.velocity = Vector2.zero;
			if( target != null )
			{
				body.AddForce( ( target.position - transform.position )
					.normalized * flySpeed,ForceMode2D.Impulse );
			}
			else
			{
				body.AddForce( vel.normalized * flySpeed,
					ForceMode2D.Impulse );
			}
		}
	}

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

	public void SetTarget( Transform target )
	{
		this.target = target;
	}

	public void SetVel( Vector2 vel )
	{
		this.vel = vel;
	}

	Transform target = null;
	Vector2 vel = Vector2.zero;
	Rigidbody2D body;

	[SerializeField] Timer lockOn = new Timer( 0.4f );
	[SerializeField] float flySpeed = 50.0f;
	[SerializeField] float deviationSpeed = 5.0f;
	bool targeted = false;
}
