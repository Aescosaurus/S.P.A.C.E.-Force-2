using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedFox
	:
	PatrolFox
{
	protected override void Start()
	{
		base.Start();

		player = GameObject.FindGameObjectWithTag( "Player" );

		var spriteTransform = transform.Find( "Sprite" );
		shotLoc1 = spriteTransform.Find( "ShotLoc1" );
		shotLoc2 = spriteTransform.Find( "ShotLoc2" );
	}

	protected override void Update()
	{
		base.Update();

		refire.Update( Time.deltaTime );

		var diff = player.transform.position - transform.position;
		if( diff.sqrMagnitude < attackRange * attackRange )
		{
			if( refire.IsDone() )
			{
				refire.Reset();

				var ang = Mathf.Atan2( diff.y,diff.x ) * Mathf.Rad2Deg;

				FireBullet( shotLoc1,ang + bulletSpread );
				FireBullet( shotLoc2,ang - bulletSpread );
			}
		}
	}

	void FireBullet( Transform shotLoc,float ang )
	{
		var bullet = Instantiate( bulletPrefab );
		bullet.transform.position = shotLoc.position;
		var rads = ang * Mathf.Deg2Rad;
		var shotVec = new Vector2( Mathf.Cos( rads ),Mathf.Sin( rads ) );
		bullet.GetComponent<Rigidbody2D>().AddForce( shotVec * shotSpd,ForceMode2D.Impulse );
		bullet.transform.eulerAngles = new Vector3( 0.0f,0.0f,ang + 180.0f );
	}

	GameObject player;
	Transform shotLoc1;
	Transform shotLoc2;

	[Header( "Ranged Fox" )]
	[SerializeField] float attackRange = 10.0f;
	[SerializeField] Timer refire = new Timer( 3.0f );
	[SerializeField] float bulletSpread = 20.0f;
	[SerializeField] GameObject bulletPrefab = null;
	[SerializeField] float shotSpd = 5.0f;
}
