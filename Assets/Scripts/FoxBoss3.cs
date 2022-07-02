using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoxBoss3
	:
	FoxBossBase
{
	enum State
	{
		LaserFrenzy,
		MissileBurst,
		Move,
		ShotgunFrenzy,
		BulletStorm
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag( "Player" );
		Assert.IsNotNull( player );
		missilePrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Missile" );
		Assert.IsNotNull( missilePrefab );
		bulletPrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Bullet" );
		Assert.IsNotNull( bulletPrefab );
		audSrc = GetComponent<AudioSource>();
		Assert.IsNotNull( audSrc );
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );

		for( int i = 0; i < guns.Length; ++i )
		{
			guns[i] = transform.Find( "Gun" + ( i + 1 ) );
			Assert.IsNotNull( guns[i] );
		}
		for( int i = 0; i < arms.Length; ++i )
		{
			arms[i] = transform.Find( "Arm" + ( i + 1 ) );
			Assert.IsNotNull( arms[i] );
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
			case State.LaserFrenzy:
				if( laserReload.Update( Time.deltaTime ) )
				{
					laserTarget = player.transform.position;
					if( randLaserArm == null )
					{
						randLaserArm = arms[Random.Range( 0,
							arms.Length )];
					}

					if( laserRefire.Update( Time.deltaTime ) )
					{
						laserRefire.Reset();

						FireBullet( randLaserArm.position,
							( laserTarget - ( Vector2 )randLaserArm.position )
							.normalized * laserMoveSpeed );

						if( ++curLaser >= laserVolleySize )
						{
							curLaser = 0;
							++curLaserVolley;
							laserReload.Reset();
							randLaserArm = null;
						}
					}

					if( curLaserVolley >= nLaserVolleys )
					{
						curLaserVolley = 0;
						action = State.MissileBurst;
					}
				}
				break;
			case State.MissileBurst:
				if( missileBurstReload.Update( Time.deltaTime ) )
				{
					missileBurstReload.Reset();
					randGun = guns[Random.Range( 0,guns.Length )];

					for( int i = 0; i < burstSize; ++i )
					{
						FireMissile( randGun.position,
							Vector2.zero,
							player.transform );
					}

					if( ++curBurst >= nBursts )
					{
						curBurst = 0;
						moveVel = new Vector2( Random.Range( -1.0f,1.0f ),
							Random.Range( -1.0f,1.0f ) );
						action = State.Move;
					}
				}
				break;
			case State.Move:
				if( moveDuration.Update( Time.deltaTime ) )
				{
					moveDuration.Reset();
					action = State.ShotgunFrenzy;
				}
				else
				{
					body.AddForce( moveVel * moveSpeed *
						Time.deltaTime );
				}
				break;
			case State.ShotgunFrenzy:
				if( shotgunRefire.Update( Time.deltaTime ) )
				{
					shotgunRefire.Reset();

					Vector2 spawn = guns[Random.Range(
						0,guns.Length )].position;
					Vector2 diff = ( Vector2 )player
						.transform.position - spawn;
					Vector2 start = Deviate( diff,
						-( nShotgunBullets / 2 ) * shotgunSpread );
					for( int i = 0; i < nShotgunBullets; ++i )
					{
						// FireMissile( spawn,start,null );
						FireBullet( spawn,start *
							shotgunBulletSpeed );
						start = Deviate( start,shotgunSpread );
					}

					if( ++curShotgunBurst >= nShotgunBursts )
					{
						curShotgunBurst = 0;
						action = State.BulletStorm;
					}
				}
				break;
			case State.BulletStorm:
				if( bulletRefire.Update( Time.deltaTime ) )
				{
					bulletRefire.Reset();

					var start = Random.Range( 0,100 ) < 50
						? guns[Random.Range( 0,guns.Length )]
						: arms[Random.Range( 0,arms.Length )];

					FireBullet( start.position,
						( player.transform.position -
						start.transform.position )
						.normalized * bulletSpeed );

					if( ++curBullet >= nBullets )
					{
						curBullet = 0;
						action = State.LaserFrenzy;
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

	Vector2 Deviate( Vector2 start,float dev )
	{
		dev *= Mathf.Deg2Rad;
		float angle = Mathf.Atan2( start.y,start.x ) + dev;
		Vector2 temp = new Vector2( Mathf.Cos( angle ),
			Mathf.Sin( angle ) );
		return ( temp );
	}

	GameObject player;
	GameObject missilePrefab;
	GameObject bulletPrefab;
	AudioSource audSrc;
	Rigidbody2D body;
	Transform[] guns = new Transform[4];
	Transform[] arms = new Transform[4];
	List<AudioClip> shootSounds = new List<AudioClip>();

	[Header( "Laser Frenzy" )]
	[SerializeField] Timer laserReload = new Timer( 1.0f );
	[SerializeField] Timer laserRefire = new Timer( 0.1f );
	[SerializeField] float laserMoveSpeed = 1.2f;
	[SerializeField] int laserVolleySize = 8;
	[SerializeField] int nLaserVolleys = 10;
	Vector2 laserTarget;
	Transform randLaserArm = null;
	int curLaserVolley = 0;
	int curLaser = 0;

	[Header( "Missile Burst" )]
	[SerializeField] Timer missileBurstReload = new Timer( 1.0f );
	[SerializeField] int burstSize = 4;
	[SerializeField] int nBursts = 10;
	Transform randGun;
	int curBurst = 0;

	[Header( "Move" )]
	[SerializeField] Timer moveDuration = new Timer( 3.0f );
	[SerializeField] float moveSpeed = 300.0f;
	Vector2 moveVel = Vector2.zero;

	[Header( "Shotgun Frenzy" )]
	[SerializeField] Timer shotgunRefire = new Timer( 1.5f );
	[SerializeField] int nShotgunBullets = 5;
	[SerializeField] float shotgunSpread = 30.0f;
	[SerializeField] float shotgunBulletSpeed = 0.9f;
	[SerializeField] int nShotgunBursts = 10;
	int curShotgunBurst = 0;

	[Header( "Bullet Storm" )]
	[SerializeField] Timer bulletRefire = new Timer( 0.05f );
	[SerializeField] int nBullets = 30;
	[SerializeField] float bulletSpeed = 1.5f;
	int curBullet = 0;

	State action = State.LaserFrenzy;
}
