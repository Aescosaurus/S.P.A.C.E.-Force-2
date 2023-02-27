using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility
	:
	PlayerAbility
{
	protected override void Start()
	{
		base.Start();

		body = transform.parent.GetComponent<Rigidbody2D>();
		dashMask = LayerMask.GetMask( "Default" );
	}

	protected override void ActivateAbility()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		Vector2 diff = mousePos - ( Vector2 )transform.position;
		diff.Normalize();

		transform.parent.rotation = Quaternion.Euler( 0.0f,0.0f,Mathf.Atan2( diff.y,diff.x ) * Mathf.Rad2Deg - 90.0f );

		body.AddForce( diff * dashForce,ForceMode2D.Impulse );
		
		var rayHit = Physics2D.Raycast( transform.parent.position,diff,dashDist + additionalDistCheck,dashMask );
		if( !rayHit ) body.MovePosition( ( Vector2 )transform.parent.position + diff * dashDist );
	}

	Rigidbody2D body;
	LayerMask dashMask;

	[SerializeField] float dashForce = 3.0f;
	[SerializeField] float dashDist = 1.5f;
	[SerializeField] float additionalDistCheck = 0.5f;
}
