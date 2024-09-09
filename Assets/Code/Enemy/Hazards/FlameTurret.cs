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
		var colls = GetComponents<BoxCollider2D>();
		foreach( var coll in colls )
		{
			if( coll.isTrigger ) damageHitbox = coll;
		}
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
		damageHitbox.enabled = enabled;

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

	void OnTriggerEnter2D( Collider2D damageHitbox )
	{
		if( damageHitbox.tag == "Player" ) damageHitbox.GetComponent<HealthBar>().Hurt( 1.0f );
	}

	ParticleSystem partSys;
	BoxCollider2D damageHitbox;
	Animator animCtrl;

	[Tooltip( "Set to 0 for infinite" )]
	[SerializeField] Timer refire = new Timer( 3.0f );
	[SerializeField] Timer flameDur = new Timer( 3.0f );

	bool activated = false;
	float animSpd = 0.0f;
}
