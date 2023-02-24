using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Assertions;

public class Boss1Attack
	:
	MonoBehaviour
{
	enum Phase
	{
		Shotguns,
		BulletSpam,
		PlayerAim
	}

	void Start()
	{
		bossBase = GetComponent<EnemyBoss>();
		player = GameObject.FindGameObjectWithTag( "Player" );

		Assert.IsNotNull( laserLeft );
		Assert.IsNotNull( laserRight );

		leftOffset = laserLeft.transform.position - transform.position;
		rightOffset = laserRight.transform.position - transform.position;

		myCollider = GetComponent<Collider2D>();
		myCollider.enabled = false;
	}

	void Update()
	{
		if( bossBase.GetActivated() )
		{
			if( laserLeft != null ) laserLeft.transform.position = ( Vector2 )transform.position + leftOffset;
			if( laserRight != null ) laserRight.transform.position = ( Vector2 )transform.position + rightOffset;

			if( laserLeft == null && laserRight == null )
			{
				// todo: display particles or something
				// slow spd & make it move at random? - attach chicken movement?
				myCollider.enabled = true;
			}

			switch( phase )
			{
				case Phase.Shotguns:
					if( !shotgunPhase.Update( Time.deltaTime ) )
					{
						if( shotgunRefire.Update( Time.deltaTime ) )
						{
							GetAlternateLaser()?.Shoot( shotgunNShots,shotgunBulletSpd );

							shotgunRefire.Reset();
						}
					}
					else
					{
						shotgunPhase.Reset();
						shotgunRefire.Reset();
						phase = Phase.BulletSpam;
					}
					break;
				case Phase.BulletSpam:
					if( !bulletSpamPhase.Update( Time.deltaTime ) )
					{
						if( bulletSpamRefire.Update( Time.deltaTime ) )
						{
							if( laserLeft != null ) laserLeft.Shoot( 1,bulletSpamBulletSpd );
							if( laserRight != null ) laserRight.Shoot( 1,bulletSpamBulletSpd );

							bulletSpamRefire.Reset();
						}
					}
					else
					{
						bulletSpamPhase.Reset();
						bulletSpamRefire.Reset();
						phase = Phase.PlayerAim;
					}
					break;
				case Phase.PlayerAim:
					if( laserLeft != null ) laserLeft.AimAt( player.transform.position );
					if( laserRight != null ) laserRight.AimAt( player.transform.position );

					if( !playerAimPhase.Update( Time.deltaTime ) )
					{
						if( playerAimRefire.Update( Time.deltaTime ) )
						{
							GetAlternateLaser()?.Shoot( 1,playerAimBulletSpd );

							playerAimRefire.Reset();
						}
					}
					else
					{
						playerAimPhase.Reset();
						playerAimRefire.Reset();
						phase = Phase.Shotguns;

						if( laserLeft != null ) laserLeft.ResetAim();
						if( laserRight != null ) laserRight.ResetAim();
					}
					break;
			}
		}
	}

	Boss1Laser GetAlternateLaser()
	{
		curAlternate = !curAlternate;

		var returnLaser = curAlternate ? laserLeft : laserRight;
		if( returnLaser == null ) return( null );
		else return( returnLaser );
	}

	EnemyBoss bossBase;
	Collider2D myCollider;
	GameObject player;

	Phase phase = Phase.Shotguns;

	[SerializeField] Boss1Laser laserLeft = null;
	[SerializeField] Boss1Laser laserRight = null;

	Vector2 leftOffset;
	Vector2 rightOffset;

	[Header( "Shotgun Phase" )]
	[SerializeField] Timer shotgunRefire = new Timer( 0.35f );
	[SerializeField] Timer shotgunPhase = new Timer( 3.5f );
	[SerializeField] int shotgunNShots = 5;
	[SerializeField] float shotgunBulletSpd = 3.0f;

	[Header( "Bullet Spam Phase" )]
	[SerializeField] Timer bulletSpamRefire = new Timer( 0.2f );
	[SerializeField] Timer bulletSpamPhase = new Timer( 1.5f );
	[SerializeField] float bulletSpamBulletSpd = 10.0f;

	[Header( "Player Aim Phase" )]
	[SerializeField] Timer playerAimRefire = new Timer( 0.8f );
	[SerializeField] Timer playerAimPhase = new Timer( 4.0f );
	[SerializeField] float playerAimBulletSpd = 5.0f;

	bool curAlternate = false;
}
