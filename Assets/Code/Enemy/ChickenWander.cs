using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenWander
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
	}

	void Update()
	{
		if( wandering )
		{
			if( wanderTimer.Update( Time.deltaTime ) )
			{
				body.velocity = Vector2.zero;
				wandering = false;
				wanderTimer.Reset();
				waitTimer.SetDur( waitDur.Rand() );
				anim.speed = 0.0f;
			}
		}
		else
		{
			if( waitTimer.Update( Time.deltaTime ) )
			{
				body.velocity = new Vector2( Random.Range( -1.0f,1.0f ),Random.Range( -1.0f,1.0f ) )
					.normalized * wanderSpd.Rand();
				
				SetXScale( Mathf.Sign( body.velocity.x ) );
				
				wandering = true;
				waitTimer.Reset();
				wanderTimer.SetDur( wanderDur.Rand() );
				anim.speed = 1.0f;
			}

			if( flipRetryTimer.Update( Time.deltaTime ) )
			{
				flipRetryTimer.Reset();
				flipRetryTimer.SetDur( flipRetryDur.Rand() );
				if( Random.Range( 0.0f,1.0f ) < flipChance )
				{
					SetXScale( -transform.localScale.x );
				}
			}
		}
	}

	void SetXScale( float scale )
	{
		var newScale = transform.localScale;
		newScale.x = scale;
		transform.localScale = newScale;
	}

	Rigidbody2D body;
	Animator anim;

	Timer wanderTimer = new Timer( 0.0f );
	Timer waitTimer = new Timer( 0.0f );

	[SerializeField] RangeF wanderDur = new RangeF( 0.2f,1.0f );
	[SerializeField] RangeF waitDur = new RangeF( 1.0f,1.5f );

	[SerializeField] RangeF wanderSpd = new RangeF( 0.2f,1.5f );

	[SerializeField] float flipChance = 0.4f;
	[SerializeField] RangeF flipRetryDur = new RangeF( 0.2f,0.5f );
	Timer flipRetryTimer = new Timer( 0.0f );

	bool wandering = false;
	Vector2 wanderDir = Vector2.zero;
}
