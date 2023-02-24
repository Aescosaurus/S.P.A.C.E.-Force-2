using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BossZoom
	:
	MonoBehaviour
{
	void Start()
	{
		camFocusScr = Camera.main.GetComponent<CameraFocus>();

		Assert.IsNotNull( myBoss );
	}

	void OnTriggerStay2D( Collider2D coll )
	{
		if( coll.tag == "Player" )
		{
			camFocusScr.SetBuffSize( zoomAmount );
			myBoss.ToggleActivate( true );
		}
	}

	void OnTriggerExit2D( Collider2D coll )
	{
		if( coll.tag == "Player" )
		{
			camFocusScr.SetBuffSize( 0.0f );
			myBoss.ToggleActivate( false );
		}
	}
	
	CameraFocus camFocusScr;

	[SerializeField] float zoomAmount = 10.0f;
	[SerializeField] EnemyBoss myBoss = null;
}
