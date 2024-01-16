using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon
	:
	MonoBehaviour
{
	void Start()
	{
		body = transform.parent.GetComponent<Rigidbody2D>();

		gun1 = transform.parent.Find( "Gun1" );
		gun2 = transform.parent.Find( "Gun2" );
	}

	void Update()
	{
		bool attacking = Input.GetAxis( "Attack" ) > 0.0f;

		if( canAttackToggle && Input.GetAxis( "AttackToggle" ) > 0 )
		{
			attackToggle = !attackToggle;
		}
		canAttackToggle = !( Input.GetAxis( "AttackToggle" ) > 0 );

		if( attackToggle ) attacking = true;

		if( refire.Update( Time.deltaTime ) && attacking )
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

		transform.parent.rotation = CalcRot( diff );

		return( diff );
	}

	void FireBullet( Vector2 pos,Vector2 moveDir )
	{
		var bull = Instantiate( bulletPrefab,
			pos,Quaternion.identity );
		bull.transform.rotation = CalcRot( moveDir );
		var bullBody = bull.GetComponent<Rigidbody2D>();
		bullBody.AddForce( moveDir * bulletSpeed,
			ForceMode2D.Impulse );

		Destroy( bull,2.0f );
	}

	Quaternion CalcRot( Vector2 diff )
	{
		return( Quaternion.Euler( 0.0f,0.0f,Mathf.Atan2( diff.y,diff.x ) * Mathf.Rad2Deg - 90.0f ) );
	}

	Rigidbody2D body;
	protected Transform gun1;
	protected Transform gun2;

	[SerializeField] Timer refire = new Timer( 0.2f );

	[SerializeField] GameObject bulletPrefab = null;
	[SerializeField] float bulletSpeed = 2.0f;
	[SerializeField] float pushForce = 1.0f;

	int curGun = 0;

	bool canAttackToggle = true;
	bool attackToggle = false;
}
