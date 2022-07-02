using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KittenSpawner
	:
	MonoBehaviour
{
	void Start()
	{
		kittenPrefab = Resources.Load<GameObject>(
			"Prefabs/Kitten" );
		Assert.IsNotNull( kittenPrefab );
	}

	void Update()
	{
		if( kittenSpawnTimer.Update( Time.deltaTime ) )
		{
			kittenSpawnTimer.Reset();

			if( Random.Range( 0.0f,100.0f ) < kittenSpawnRate )
			{
				var rand = new Vector2( Random.Range( -1.0f,1.0f ),
					Random.Range( -1.0f,1.0f ) );
				if( rand.x == 0.0f ) rand.x = 1.0f;
				if( rand.y == 0.0f ) rand.y = 1.0f;
				var loc = rand.normalized * spawnRange;

				var kitten = Instantiate( kittenPrefab,
					transform );
				kitten.transform.position = loc;
			}
		}
	}

	GameObject kittenPrefab;

	[Tooltip( "Chance of spawning a kitten every second." )]
	[Range( 0.0f,100.0f )]
	[SerializeField] float kittenSpawnRate = 0.0f;
	[SerializeField] float spawnRange = 0.0f;

	Timer kittenSpawnTimer = new Timer( 1.0f );
}
