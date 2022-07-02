using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AsteroidSpawner
	:
	MonoBehaviour
{
	void Start()
	{
		asteroidPrefab = Resources.Load<GameObject>(
			"Prefabs/Asteroid" );
		Assert.IsNotNull( asteroidPrefab );

		int tries = nBorderAsteroids;
		for( int i = 0; i < nBorderAsteroids; ++i )
		{
			var rand = new Vector2( Random.Range( -1.0f,1.0f ),
				Random.Range( -1.0f,1.0f ) );
			if( rand.x == 0.0f ) rand.x = 1.0f;
			if( rand.y == 0.0f ) rand.y = 1.0f;
			var astPos = rand.normalized * Random.Range(
				minBoundsSpawnRange,maxBoundsSpawnRange );

			if( !CreateAsteroid( astPos ) && tries > 0 )
			{
				--tries;
				--i;
			}
		}

		for( int i = 0; i < nRandomAsteroids; ++i )
		{
			var rand = new Vector2( Random.Range( -1.0f,1.0f ),
				Random.Range( -1.0f,1.0f ) );
			if( rand.x == 0.0f ) rand.x = 1.0f;
			if( rand.y == 0.0f ) rand.y = 1.0f;
			var astPos = rand.normalized * Random.Range(
				0.0f,minBoundsSpawnRange - 1.0f );

			CreateAsteroid( astPos );
		}
	}

	bool CreateAsteroid( Vector2 loc )
	{
		var spr = asteroidSprites[Random.Range( 0,
			asteroidSprites.Length )];
		var astSize = spr.bounds.size.x / 2.0f;

		if( Physics2D.OverlapCircle( loc,astSize ) == null )
		{
			var ast = Instantiate( asteroidPrefab,
				loc,Quaternion.identity );
			ast.GetComponent<SpriteRenderer>().sprite = spr;
			ast.GetComponent<CircleCollider2D>().radius =
				astSize;
			ast.transform.Rotate( 0.0f,0.0f,
				Random.Range( 0.0f,360.0f ) );
			return( true );
		}
		else
		{
			return( false );
		}
	}

	GameObject asteroidPrefab;

	[SerializeField] public float minBoundsSpawnRange = 0.0f;
	[SerializeField] float maxBoundsSpawnRange = 0.0f;
	[SerializeField] int nBorderAsteroids = 0;
	[SerializeField] int nRandomAsteroids = 0;
	[Header( "Sprites" )]
	[SerializeField] Sprite[] asteroidSprites = {};
}
