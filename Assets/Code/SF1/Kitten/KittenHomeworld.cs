using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KittenHomeworld
	:
	MonoBehaviour
{
	void Start()
	{
		audSrc = GetComponent<AudioSource>();
		Assert.IsNotNull( audSrc );

		kittenSaveSound = Resources.Load<AudioClip>(
			"Sounds/Kitten Saved" );
		Assert.IsNotNull( kittenSaveSound );
	}

	void Update()
	{
		transform.Rotate( 0.0f,0.0f,rotSpeed * Time.deltaTime );
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.tag == "SpaceKitten" )
		{
			audSrc.PlayOneShot( kittenSaveSound );
			Destroy( coll.gameObject );
			LevelHandler.SaveKitty();

			var enemies = GameObject
				.FindGameObjectsWithTag( "SpaceFox" );
			foreach( var enemy in enemies )
			{
				var diff = enemy.transform.position -
					transform.position;
				if( diff.sqrMagnitude <
					pushBackDistance * pushBackDistance )
				{
					enemy.GetComponent<Rigidbody2D>()
						.AddForce( diff.normalized *
						pushBackPower,ForceMode2D.Impulse );
				}
			}
		}
	}

	AudioSource audSrc;

	[SerializeField] float rotSpeed = 0.0f;
	[SerializeField] float pushBackDistance = 6.0f;
	[SerializeField] float pushBackPower = 4.0f;

	AudioClip kittenSaveSound;
}
