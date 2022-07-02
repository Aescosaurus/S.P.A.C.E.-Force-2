using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Star
	:
	MonoBehaviour
{
	void Start()
	{
		parallaxSpeed = GetComponent<SpriteRenderer>()
			.bounds.size.x / 2.0f *
			Random.Range( 0.9f,1.1f );

		cam = Camera.main.GetComponent<CameraMove>();
		Assert.IsNotNull( cam );
	}

	void Update()
	{
        if (cam != null)
        {
            transform.position += (Vector3)cam.GetDelta() *
                parallaxSpeed;
        }
	}

	float parallaxSpeed;
	CameraMove cam;
}
