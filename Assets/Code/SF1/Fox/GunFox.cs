using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GunFox
	:
	MonoBehaviour
{
	void Start()
	{
		player = GameObject.FindGameObjectWithTag(
			"Player" );
		Assert.IsNotNull( player );
		gun1 = transform.Find( "Gun1" );
		Assert.IsNotNull( gun1 );
		gun2 = transform.Find( "Gun2" );
		Assert.IsNotNull( gun2 );
		bulletPrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Bullet" );
		Assert.IsNotNull( bulletPrefab );
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );

		reload.Update( Random.Range( 0.0f,5.0f ) );
	}

	void Update()
	{
		Vector2 diff = player.transform.position -
			transform.position;
		reload.Update( Time.deltaTime );
		if( diff.sqrMagnitude < attackRange * attackRange )
		{
			if( reload.IsDone() )
			{
				if( refire.Update( Time.deltaTime ) )
				{
					refire.Reset();

					var bullet = Instantiate( bulletPrefab );
					bullet.transform.position = curShot == 0
						? gun1.position : gun2.position;
					bullet.GetComponent<Rigidbody2D>()
						.AddForce( diff.normalized *
						bulletSpeed,ForceMode2D.Impulse );
					// audSrc.PlayOneShot( shootSounds[Random
					// 	.Range( 0,shootSounds.Count )] );

					if( ++curShot > 1 )
					{
						curShot = 0;
						reload.Reset();
					}
				}
			}
		}
		else if( body.velocity.sqrMagnitude <
			maxSpeed * maxSpeed )
		{
			body.AddForce( diff.normalized * moveSpeed *
				Time.deltaTime );
		}

		if( body.velocity.x != 0.0f )
		{
			var scale = transform.localScale;
			scale.x = body.velocity.x / Mathf.Abs( body.velocity.x );
			transform.localScale = scale;
		}
	}

	GameObject player;
	Transform gun1;
	Transform gun2;
	GameObject bulletPrefab;
	Rigidbody2D body;

	int curShot = 0;

	[SerializeField] float attackRange = 7.0f;
	[SerializeField] Timer refire = new Timer( 1.0f );
	[SerializeField] Timer reload = new Timer( 5.0f );
	[SerializeField] float moveSpeed = 50.0f;
	[SerializeField] float bulletSpeed = 10.0f;
	[SerializeField] float maxSpeed = 100.0f;
}
