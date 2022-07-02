using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid
	:
	MonoBehaviour
{
	void Update()
	{
		if( asteroidSpawner == null )
		{
			asteroidSpawner = GameObject.Find(
				"Asteroid Spawner" );
		}
		transform.SetParent( asteroidSpawner.transform );

		Destroy( GetComponent<Asteroid>() );
	}

	static GameObject asteroidSpawner = null;
}
