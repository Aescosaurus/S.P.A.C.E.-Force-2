using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraMove
	:
	MonoBehaviour
{
	void Start()
	{
		player = GameObject.FindGameObjectWithTag( "Player" );
		Assert.IsNotNull( player );

		lastPos = transform.position;
	}

	void FixedUpdate()
	{
        if (player != null)
        {
            Vector2 diff = player.transform.position -
                transform.position;

            transform.position += (Vector3)diff;

            delta = (Vector2)transform.position - lastPos;

            lastPos = transform.position;
        }
	}

	public Vector2 GetDelta()
	{
		return( delta );
	}

	Vector2 lastPos;
	Vector2 delta;
	GameObject player;
}
