using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpike
	:
	MonoBehaviour
{
	void Start()
	{
		warningParts = transform.Find( "WarningParticles" ).GetComponent<ParticleSystem>();
		launchParts = transform.Find( "LaunchParticles" ).GetComponent<ParticleSystem>();
		var em = launchParts.emission;
		em.enabled = false;

		raycastPos = transform.Find( "RaycastPos" );

		raycastInterval.Randomize();

		hitLayer = LayerMask.GetMask( "Default","Emancipator" );
	}

	void Update()
	{
		if( inactive ) return;

		if( triggered )
		{
			if( warningDur.Update( Time.deltaTime ) )
			{
				var body = gameObject.AddComponent<Rigidbody2D>();
				body.gravityScale = 0.0f;
				body.AddForce( transform.up * launchForce,ForceMode2D.Impulse );
				
				var warningEm = warningParts.emission;
				warningEm.rateOverTime = 0.0f;
				var launchEm = launchParts.emission;
				launchEm.rateOverTime = 0.0f;

				inactive = true;
			}
		}
		else if( raycastInterval.Update( Time.deltaTime ) )
		{
			raycastInterval.Reset();

			var result = Physics2D.Raycast( raycastPos.position,raycastPos.up );
			if( result && result.transform.tag == "Player" )
			{
				var em = launchParts.emission;
				em.enabled = true;
				triggered = true;
			}
		}
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		// todo: spawn blue explosion particles here

		if( coll.gameObject.tag == "Player" ) coll.gameObject.GetComponent<HealthBar>().Hurt( 1.0f );

		if( ( hitLayer & ( 1 << coll.gameObject.layer ) ) != 0 )
		{
			coll.gameObject.GetComponent<AsteroidSplit>()?.BreakOpen( gameObject );
			PartHand.Get().SpawnParts( transform.position,PartHand.PartType.CrystalSpikeExplode );
			Destroy( gameObject );
		}
	}

	ParticleSystem warningParts;
	ParticleSystem launchParts;
	Transform raycastPos;

	LayerMask hitLayer;

	[SerializeField] Timer raycastInterval = new Timer( 0.2f );
	[SerializeField] float launchForce = 1.0f;
	[SerializeField] Timer warningDur = new Timer( 0.5f );

	bool triggered = false;
	bool inactive = false;
}
