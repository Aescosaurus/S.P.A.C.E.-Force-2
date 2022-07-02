using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody2D>();

		gun1 = transform.Find( "Gun1" );
		gun2 = transform.Find( "Gun2" );
	}

	void Update()
	{
		if( refire.Update( Time.deltaTime ) &&
			Input.GetAxis( "Attack" ) > 0.0f )
		{
			refire.Reset();

			Vector2 diff = RotateGetDiff();

			body.AddForce( -diff * pushForce,
				ForceMode2D.Impulse );

			if( ++curGun > 1 ) curGun = 0;

			FireBullet( curGun == 0
				? gun1.position
				: gun2.position,
				diff );
		}
	}

	Vector2 RotateGetDiff()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(
			Input.mousePosition );
		Vector2 diff = mousePos - ( Vector2 )transform.position;
		diff.Normalize();

		transform.rotation = Quaternion.Euler( 0.0f,0.0f,
			Mathf.Atan2( diff.y,diff.x ) * Mathf.Rad2Deg - 90.0f );

		return( diff );
	}

	void FireBullet( Vector2 pos,Vector2 moveDir )
	{
		var bull = Instantiate( bulletPrefab,
			pos,Quaternion.identity );
		var bullBody = bull.GetComponent<Rigidbody2D>();
		bullBody.AddForce( moveDir * bulletSpeed,
			ForceMode2D.Impulse );
	}

	Rigidbody2D body;
	protected Transform gun1;
	protected Transform gun2;

	[SerializeField] Timer refire = new Timer( 0.2f );

	[SerializeField] GameObject bulletPrefab = null;
	[SerializeField] float bulletSpeed = 2.0f;
	[SerializeField] float pushForce = 1.0f;

	int curGun = 0;
}
