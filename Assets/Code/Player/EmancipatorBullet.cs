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
		PartHand.Get().SpawnPartsDirectional( transform.position,-vel,
			PartHand.PartType.BulletHit );

		if( coll.gameObject.tag == "SpaceFox" || coll.gameObject.tag == "Breakable" )
		{
			coll.gameObject.GetComponent<HealthBar>()?.Hurt( 1 );
		}
		Destroy( gameObject );
	}

	public void Fire( Vector2 force )
	{
		var body = GetComponent<Rigidbody2D>();
		body.AddForce( force,ForceMode2D.Impulse );
		vel = force;
	}

	Vector2 vel = Vector2.zero;

	[SerializeField] float rotationSpeedMin = 0.0f;
	[SerializeField] float rotationSpeedMax = 0.0f;

	float rotationSpeed;
}
