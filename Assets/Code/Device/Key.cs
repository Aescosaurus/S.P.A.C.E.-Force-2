using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key
	:
	MonoBehaviour
{
	void Start()
	{
		moveThresh = moveThreshRange.Rand();
		lerpAmount = lerpRange.Rand();
	}

	void Update()
	{
		if( following )
		{
			if( ( followTarget.transform.position - transform.position ).sqrMagnitude > moveThresh * moveThresh )
			{
				transform.position = Vector2.Lerp( transform.position,followTarget.transform.position,
					Mathf.Min( 1.0f,lerpAmount * Time.deltaTime ) );
			}
		}
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		if( following ) return;

		if( coll.tag == "Player" )
		{
			following = true;
			followTarget = coll.gameObject;
			coll.GetComponent<PlayerHeldItems>().PickupKey( gameObject );
			GetComponent<ParticleSystem>().Emit( 10 );
		}
	}

	bool following = false;
	GameObject followTarget = null;

	// [SerializeField] float moveThresh = 1.2f;
	[SerializeField] RangeF moveThreshRange = new RangeF( 0.8f,1.6f );
	float moveThresh;
	// [SerializeField] float lerpAmount = 0.3f;
	float lerpAmount;
	[SerializeField] RangeF lerpRange = new RangeF( 0.2f,0.6f );
}
