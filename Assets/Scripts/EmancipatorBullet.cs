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

		explosionPrefab = Resources.Load<GameObject>(
			"Prefabs/Explosion" );
		Assert.IsNotNull( explosionPrefab );

		explodeSound = Resources.Load<AudioClip>(
			"Sounds/Asteroid Hit" );
		Assert.IsNotNull( explodeSound );
	}

	void Update()
	{
		transform.Rotate( 0.0f,0.0f,
			rotationSpeed * Time.deltaTime );

		// if( despawn.Update( Time.deltaTime ) )
		// {
		// 	Destroy( gameObject );
		// }
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		// TODO: Deal damage to evil foxes if you hit one.
		if( coll.gameObject.tag == "SpaceFox" )
		{
			coll.gameObject.GetComponent<HealthBar>()
				.Hurt( 1 );
		}
		else
		{
			var explosion = Instantiate( explosionPrefab );
			var clip = explosion.GetComponent<AudioSource>();
			clip.clip = explodeSound;
			clip.volume = 0.13f;
			clip.Play();
		}
		Destroy( gameObject );
	}

	GameObject explosionPrefab;
	AudioClip explodeSound;

	// [SerializeField] Timer despawn = new Timer( 3.5f );
	[SerializeField] float rotationSpeedMin = 0.0f;
	[SerializeField] float rotationSpeedMax = 0.0f;

	float rotationSpeed;
}
