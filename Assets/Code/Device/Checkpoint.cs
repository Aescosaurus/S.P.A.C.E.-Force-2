using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint
	:
	MonoBehaviour
{
	void Start()
	{
		partSys = GetComponent<ParticleSystem>();
		spr = transform.Find( "Checkpoint" ).gameObject;
		sprActivated = transform.Find( "CheckpointActivated" ).gameObject;

		ToggleActive( false );
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.tag == "Player" )
		{
			if( !activated )
			{
				partSys.Emit( 15 );
				ToggleActive( true );
				coll.GetComponent<PlayerCheckpoint>().ActivateCheckpoint( this );
			}
		}
	}

	public void ToggleActive( bool active )
	{
		activated = active;
		spr.SetActive( !active );
		sprActivated.SetActive( active );

		var emission = partSys.emission;
		emission.rateOverTime = new ParticleSystem.MinMaxCurve( active ? 0.0f : 10.0f );
	}

	ParticleSystem partSys;
	GameObject spr;
	GameObject sprActivated;

	bool activated = false;
}
