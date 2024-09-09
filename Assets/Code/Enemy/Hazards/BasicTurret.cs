using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret
	:
	MonoBehaviour
{
	void Start()
	{
		gunSpot = transform.Find( "GunSpot" );

		if( startRandomized ) refire.Randomize();
	}

	void Update()
	{
		if( refire.Update( Time.deltaTime ) )
		{
			FireBullet();

			refire.Reset();
		}
	}

	void FireBullet()
	{
		var bullet = Instantiate( bulletPrefab,gunSpot.position,Quaternion.identity );

		bullet.GetComponent<Rigidbody2D>().AddForce( -transform.right * shotSpd,ForceMode2D.Impulse );

		Destroy( bullet,5.0f );
	}

	Transform gunSpot;

	[SerializeField] Timer refire = new Timer( 2.0f );
	[SerializeField] GameObject bulletPrefab = null;
	[SerializeField] float shotSpd = 1.0f;
	[SerializeField] bool startRandomized = true;
}
