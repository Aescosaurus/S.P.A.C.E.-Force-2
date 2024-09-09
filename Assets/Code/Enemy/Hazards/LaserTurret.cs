using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret
	:
	MonoBehaviour
{
	void Start()
	{
		player = GameObject.FindGameObjectWithTag( "Player" );
		turretTop = transform.Find( "TurretTop" );
		shotLoc = turretTop.Find( "ShotLoc" );

		barrageRefire.Finish();
		refire.Finish();
	}

	void Update()
	{
		var diff = player.transform.position - transform.position;
		if( diff.sqrMagnitude < activateDist * activateDist )
		{
			var targetAng = Mathf.Atan2( diff.y,diff.x ) * Mathf.Rad2Deg;
			var curAng = turretTop.eulerAngles.z;
			var lerpAng = Mathf.MoveTowardsAngle( curAng,targetAng,rotSpd * Time.deltaTime );
			turretTop.rotation = Quaternion.AngleAxis( lerpAng,Vector3.forward );

			if( barrageRefire.Update( Time.deltaTime ) )
			{
				if( refire.Update( Time.deltaTime ) )
				{
					refire.Reset();
					if( ++curShot > barrageSize )
					{
						curShot = 0;
						barrageRefire.Reset();
					}
					else
					{
						var bulletVel = turretTop.right * bulletSpeed;
						var ang = Mathf.Atan2( bulletVel.y,bulletVel.x ) * Mathf.Rad2Deg + 180.0f;
						var bullet = Instantiate( bulletPrefab,shotLoc.position,Quaternion.AngleAxis( ang,Vector3.forward ) );
						bullet.GetComponent<Rigidbody2D>().AddForce( bulletVel,ForceMode2D.Impulse );
					}
				}
			}
		}
	}

	GameObject player;
	Transform turretTop;
	Transform shotLoc;

	[SerializeField] GameObject bulletPrefab = null;
	[SerializeField] float bulletSpeed = 0.4f;

	[SerializeField] Timer barrageRefire = new Timer( 5.0f );
	[SerializeField] Timer refire = new Timer( 0.5f );
	[SerializeField] int barrageSize = 3;
	int curShot = 0;

	[SerializeField] float activateDist = 4.5f;

	// How many seconds it takes to rotate to player.
	[SerializeField] float rotSpd = 0.2f;
}
