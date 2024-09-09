using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoxBoss2
	:
	FoxBossBase
{
	enum State
	{
		MissileShotgun,
		Move,
		Lasers,
		MissileBurst
	}

	void Start()
	{
		missilePrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Missile" );
		Assert.IsNotNull( missilePrefab );
		player = GameObject.FindGameObjectWithTag( "Player" );
		Assert.IsNotNull( "Player" );
		bulletPrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Bullet" );
		Assert.IsNotNull( bulletPrefab );
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );
		audSrc = GetComponent<AudioSource>();
		Assert.IsNotNull( audSrc );

		for( int i = 0; i < 3; ++i )
		{
			shotgunSpawns[i] = transform.Find( "Shotgun" +
				( i + 1 ).ToString() );
		}
		for( int i = 0; i < 2; ++i )
		{
			laserSpawns[i] = transform.Find( "Laser" +
				( i + 1 ).ToString() );
		}
		for( int i = 0; i < 4; ++i )
		{
			shootSounds.Add( Resources.Load<AudioClip>(
				"Sounds/Boss Shoot 0" + ( i + 1 ) ) );
			Assert.IsNotNull( shootSounds[i] );
		}
	}

	void Update()
	{
		switch( action )
		{
			case State.MissileShotgun:
				if( shotgunRefire.Update( Time.deltaTime ) )
				{
					shotgunRefire.Reset();

					Vector2 spawn = shotgunSpawns[Random.Range(
						0,shotgunSpawns.Length )].position;
					Vector2 diff = ( Vector2 )player
						.transform.position - spawn;
					Vector2 start = Deviate( diff,
						-( nShotgunBullets / 2 ) * shotgunSpread );
					for( int i = 0; i < nShotgunBullets; ++i )
					{
						FireMissile( spawn,start,null );
						start = Deviate( start,shotgunSpread );
					}

					if( ++curShotgunBurst >= shotgunSpawns.Length )
					{
						curShotgunBurst = 0;
						action = State.Move;
						moveVel = new Vector2( Random.Range( -1.0f,1.0f ),
							Random.Range( -1.0f,1.0f ) );
					}
				}
				break;
			case State.Move:
				if( moveDuration.Update( Time.deltaTime ) )
				{
					moveDuration.Reset();
					action = State.Lasers;
				}
				else
				{
					body.AddForce( moveVel * moveSpeed *
						Time.deltaTime );
				}
				break;
			case State.Lasers:
				if( laserReload.Update( Time.deltaTime ) )
				{
					laserTarget = player.transform.position;

					if( laserRefire.Update( Time.deltaTime ) )
					{
						laserRefire.Reset();

						FireBullet( laserSpawns[curLaserVolley].position,
							( laserTarget - ( Vector2 )laserSpawns[
							curLaserVolley].position )
							.normalized * laserMoveSpeed );

						if( ++curLaser >= laserVolleySize )
						{
							curLaser = 0;
							++curLaserVolley;
							laserReload.Reset();
						}
					}

					if( curLaserVolley >= laserSpawns.Length )
					{
						curLaserVolley = 0;
						action = State.MissileBurst;
					}
				}
				break;
			case State.MissileBurst:
				if( burstReload.Update( Time.deltaTime ) )
				{
					burstReload.Reset();

					for( int i = 0; i < burstSize; ++i )
					{
						FireMissile( shotgunSpawns[Random
							.Range( 0,shotgunSpawns.Length )]
							.position,Vector2.zero,
							player.transform );
					}

					if( ++curBurst >= nBursts )
					{
						curBurst = 0;
						action = State.MissileShotgun;
					}
				}
				break;
		}
	}

	void FireMissile( Vector2 loc,Vector2 vel,Transform target )
	{
		var missile = Instantiate( missilePrefab );
		missile.transform.position = loc;
		var scr = missile.GetComponent<FoxMissile>();
		scr.SetVel( vel );
		scr.SetTarget( target );

		audSrc.PlayOneShot( shootSounds[Random
			.Range( 0,shootSounds.Count )],0.5f );
	}

	void FireBullet( Vector2 loc,Vector2 vel )
	{
		var bullet = Instantiate( bulletPrefab );
		bullet.transform.position = loc;
		bullet.GetComponent<Rigidbody2D>()
			.AddForce( vel,ForceMode2D.Impulse );

		audSrc.PlayOneShot( shootSounds[Random
			.Range( 0,shootSounds.Count )],0.5f );
	}

	// void OnDestroy()
	// {
	// 	LevelHandler.SaveKitty();
	// }

	Vector2 Deviate( Vector2 start,float dev )
	{
		dev *= Mathf.Deg2Rad;
		float angle = Mathf.Atan2( start.y,start.x ) + dev;
		Vector2 temp = new Vector2( Mathf.Cos( angle ),
			Mathf.Sin( angle ) );
		return( temp );
	}

	GameObject missilePrefab;
	GameObject player;
	GameObject bulletPrefab;
	Rigidbody2D body;
	AudioSource audSrc;

	[Header( "Missile Shotgun" )]
	[SerializeField] int nShotgunBullets = 5;
	[SerializeField] float shotgunSpread = 35.0f;
	[SerializeField] Timer shotgunRefire = new Timer( 2.0f );
	Transform[] shotgunSpawns = new Transform[3];
	int curShotgunBurst = 0;

	[Header( "Move" )]
	[SerializeField] Timer moveDuration = new Timer( 3.0f );
	[SerializeField] float moveSpeed = 25.0f;
	Vector2 moveVel = Vector2.zero;

	[Header( "Lasers" )]
	[SerializeField] int laserVolleySize = 3;
	[SerializeField] Timer laserRefire = new Timer( 0.1f );
	[SerializeField] Timer laserReload = new Timer( 1.5f );
	[SerializeField] float laserMoveSpeed = 1.2f;
	Transform[] laserSpawns = new Transform[2];
	int curLaserVolley = 0;
	int curLaser = 0;
	Vector2 laserTarget = Vector2.zero;

	[Header( "Missile Burst" )]
	[SerializeField] int nBursts = 5;
	[SerializeField] Timer burstReload = new Timer( 1.0f );
	[SerializeField] int burstSize = 4;
	int curBurst = 0;

	State action = State.MissileShotgun;
	List<AudioClip> shootSounds = new List<AudioClip>();
}
