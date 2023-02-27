using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EmancipatorBullet
	:
	MonoBehaviour
{
	void Start()
	{
		rotationSpeed = Random.Range( rotationSpeedMin,
			rotationSpeedMax );
	}

	void Update()
	{
		transform.Rotate( 0.0f,0.0f,
			rotationSpeed * Time.deltaTime );
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.tag == "SpaceFox" || coll.gameObject.tag == "Breakable" )
		{
			coll.gameObject.GetComponent<HealthBar>()?.Hurt( 1 );
		}
		Destroy( gameObject );
	}

	[SerializeField] float rotationSpeedMin = 0.0f;
	[SerializeField] float rotationSpeedMax = 0.0f;

	float rotationSpeed;
}
