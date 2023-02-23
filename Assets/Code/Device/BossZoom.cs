using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZoom
	:
	MonoBehaviour
{
	void Start()
	{
		camFocusScr = Camera.main.GetComponent<CameraFocus>();
	}

	void OnTriggerStay2D( Collider2D coll )
	{
		if( coll.tag == "Player" )
		{
			camFocusScr.SetBuffSize( zoomAmount );
		}
	}

	void OnTriggerExit2D( Collider2D coll )
	{
		if( coll.tag == "Player" )
		{
			camFocusScr.SetBuffSize( 0.0f );
		}
	}
	
	CameraFocus camFocusScr;

	[SerializeField] float zoomAmount = 10.0f;
}
