using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Laser
	:
	MonoBehaviour
{
	void Start()
	{
		shotLoc = transform.Find( "ShotLoc" );
	}

	public void AimAt( Vector2 target )
	{
		var diff = target - ( Vector2 )transform.position;
		var rot = Mathf.Atan2( diff.y,diff.x ) * Mathf.Rad2Deg + 90.0f;

		transform.eulerAngles = new Vector3( 0.0f,0.0f,rot );
	}

	public void ResetAim()
	{
		transform.eulerAngles = Vector3.zero;
	}

	// shoot using angle of self
	public void Shoot( int nShots,float bulletSpd )
	{
		var baseAng = transform.eulerAngles.z;

		for( int i = -nShots / 2; i < nShots / 2 + 1; ++i )
		{
			var ang = ( baseAng + ( float )i * bulletSpread - 90.0f ) * Mathf.Deg2Rad;
			var forceVec = new Vector3( Mathf.Cos( ang ),Mathf.Sin( ang ),0.0f );

			var curBullet = Instantiate( bulletPrefab );
			curBullet.transform.localScale = Vector2.one * bulletScale;
			curBullet.transform.position = shotLoc.position;
			curBullet.transform.eulerAngles = new Vector3( 0.0f,0.0f,
				Mathf.Atan2( forceVec.y,forceVec.x ) * Mathf.Rad2Deg + 180.0f );
			curBullet.GetComponent<Rigidbody2D>().AddForce( forceVec * bulletSpd,ForceMode2D.Impulse );
		}
	}

	Transform shotLoc;

	[SerializeField] GameObject bulletPrefab = null;
	[SerializeField] float bulletSpread = 10.0f;
	[SerializeField] float bulletScale = 1.25f;
}
