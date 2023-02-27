using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FlameTurret
	:
	MonoBehaviour
{
	void Start()
	{
		partSys = GetComponentInChildren<ParticleSystem>();
		coll = GetComponent<BoxCollider2D>();
		animCtrl = GetComponentInChildren<Animator>();

		if( refire.GetDur() > 0.0f ) animSpd = 1.0f / refire.GetDur();

		refire.Finish();
		// ToggleEnabled( true );
	}

	void Update()
	{
		if( refire.Update( Time.deltaTime ) )
		{
			if( flameDur.Update( Time.deltaTime ) )
			{
				activated = false;

				ToggleEnabled( false );
				
				flameDur.Reset();
				refire.Reset();
			}
			else
			{
				if( !activated )
				{
					activated = true;

					ToggleEnabled( true );

					animCtrl.speed = 0.0f;
				}
			}
		}
		else
		{
			animCtrl.SetFloat( "NormalizedTime",refire.GetPercent() );
		}
	}

	void ToggleEnabled( bool enabled )
	{
		coll.enabled = enabled;

		if( enabled )
		{
			partSys.Play();
			animCtrl.SetFloat( "NormalizedTime",0.0f );
		}
		else
		{
			partSys.Stop();
		}
	}

	ParticleSystem partSys;
	BoxCollider2D coll;
	Animator animCtrl;

	[SerializeField] Timer refire = new Timer( 3.0f );
	[SerializeField] Timer flameDur = new Timer( 3.0f );

	bool activated = false;
	float animSpd = 0.0f;
}
