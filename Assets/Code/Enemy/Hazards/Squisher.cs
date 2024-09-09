using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squisher
	:
	MonoBehaviour
{
	enum SquishState
	{
		WaitForExtend,
		Extending,
		ExtendPause,
		Returning
	}
	void Start()
	{
		squisherTop = transform.Find( "SquisherTop" );
		// squisherBody = squisherTop.GetComponent<Rigidbody2D>();
		hitbox = squisherTop.GetComponent<BoxCollider2D>();
		hitbox.isTrigger = false;

		startPos = squisherTop.localPosition;
		startPos.y = basePos;
		endPos = squisherTop.localPosition;
		endPos.y = maxExtend;
	}

	void Update()
	{
		switch( state )
		{
			case SquishState.WaitForExtend:
				if( extendRefire.Update( Time.deltaTime ) )
				{
					extendRefire.Reset();
					// damaging = true;
					state = SquishState.Extending;
					// squisherBody.AddForce( transform.up * extendForce,ForceMode2D.Impulse );
					hitbox.isTrigger = true;
				}
				break;
			case SquishState.Extending:
				if( extendSpeed.Update( Time.deltaTime ) )
				{
					// squisherBody.velocity = Vector3.zero;
					
					// var pos = squisherTop.transform.localPosition;
					// pos.y = maxExtend;
					// squisherTop.transform.localPosition = pos;
					extendSpeed.Reset();

					squisherTop.localPosition = endPos;
					
					hitbox.isTrigger = false;

					state = SquishState.ExtendPause;
				}
				else
				{
					squisherTop.transform.localPosition = Vector3.Lerp(
						startPos,endPos,extendSpeed.GetPercent() );
				}
				break;
			case SquishState.ExtendPause:
				if( extendStay.Update( Time.deltaTime ) )
				{
					extendStay.Reset();
					// squisherBody.AddForce( -transform.up * returnForce,ForceMode2D.Impulse );

					state = SquishState.Returning;
				}
				break;
			case SquishState.Returning:
				if( returnSpeed.Update( Time.deltaTime ) )
				{
					// squisherBody.velocity = Vector3.zero;
					
					// var pos = squisherTop.transform.localPosition;
					// pos.y = basePos;
					// squisherTop.transform.localPosition = pos;
					returnSpeed.Reset();

					squisherTop.localPosition = startPos;

					state = SquishState.WaitForExtend;
				}
				else
				{
					squisherTop.transform.localPosition = Vector3.Lerp(
						endPos,startPos,returnSpeed.GetPercent() );
				}
				break;
		}
	}

	Transform squisherTop;
	// Rigidbody2D squisherBody;
	BoxCollider2D hitbox;

	const float basePos = 1.0f / 16.0f;
	const float maxExtend = 1.0f - ( 1.0f / 16.0f );

	Vector3 startPos;
	Vector3 endPos;

	// [SerializeField] float extendForce = 1.0f;
	[SerializeField] Timer extendRefire = new Timer( 3.5f );
	[SerializeField] Timer extendSpeed = new Timer( 0.4f );
	[SerializeField] Timer extendStay = new Timer( 0.5f );
	[SerializeField] Timer returnSpeed = new Timer( 1.0f );
	// [SerializeField] float returnForce = 0.4f;

	SquishState state = SquishState.WaitForExtend;
}
