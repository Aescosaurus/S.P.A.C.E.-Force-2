using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoxExploder
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );
		player = GameObject.FindGameObjectWithTag( "Player" );
		Assert.IsNotNull( player );
		healthBar = GetComponent<HealthBar>();
		Assert.IsNotNull( healthBar );
		bulletPrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Bullet" );
	}

	void Update()
	{
		Vector2 diff = player.transform.position -
			transform.position;
		body.AddForce( diff.normalized * moveSpeed *
			Time.deltaTime );

		if( healthBar.GetHealth() <= 1 )
		{
			healthBar.Hurt( 1 );
			Vector2 start = Vector2.up;
			const int nShots = 8;
			for( int i = 0; i < nShots; ++i )
			{
				var bullet = Instantiate( bulletPrefab );
				bullet.transform.position = transform.position;
				bullet.GetComponent<Rigidbody2D>()
					.AddForce( start,ForceMode2D.Impulse );

				start = Deviate( start,360.0f / ( float )nShots );
			}
		}

		if( body.velocity.x != 0.0f )
		{
			var scale = transform.localScale;
			scale.x = body.velocity.x / Mathf.Abs( body.velocity.x );
			transform.localScale = scale;
		}
	}

	Vector2 Deviate( Vector2 start,float dev )
	{
		dev *= Mathf.Deg2Rad;
		float angle = Mathf.Atan2( start.y,start.x ) + dev;
		Vector2 temp = new Vector2( Mathf.Cos( angle ),
			Mathf.Sin( angle ) );
		return ( temp );
	}

	Rigidbody2D body;
	GameObject player;
	HealthBar healthBar;
	GameObject bulletPrefab;

	[SerializeField] float moveSpeed = 500.0f;
}
