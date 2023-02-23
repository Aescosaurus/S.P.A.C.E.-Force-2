using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFocus
	:
	MonoBehaviour
{
	void Start()
	{
		player = GameObject.FindGameObjectWithTag(
			"Player" );
		Assert.IsNotNull( player );
		body = player.GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );
		cam = GetComponent<Camera>();
		Assert.IsNotNull( cam );

		startSize = cam.orthographicSize;
	}

	void Update()
	{
		// cam.orthographicSize = startSize +
		// 	body.velocity.magnitude / scaleFactor;
		cam.orthographicSize = Vector2.Lerp(
			Vector2.right * cam.orthographicSize,
			Vector2.right * ( ( startSize - offset ) + body.velocity.magnitude / scaleFactor + buffSize ),
			scaleSpeed * Time.deltaTime ).x;
	}

	public void SetBuffSize( float size )
	{
		buffSize = size;
	}

	GameObject player;
	Rigidbody2D body;
	Camera cam;

	float startSize;
	[SerializeField] float scaleFactor = 4.0f;
	[SerializeField] float scaleSpeed = 0.2f;
	[SerializeField] float offset = 1.0f;

	float buffSize = 0.0f;
}
