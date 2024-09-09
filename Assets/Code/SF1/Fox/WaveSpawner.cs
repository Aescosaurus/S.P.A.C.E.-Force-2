using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaveSpawner
	:
	MonoBehaviour
{
	void Start()
	{
		curWaveSize = waveSize;

		Assert.IsTrue( enemies.Length > 1 );

		audSrc = GetComponent<AudioSource>();
		Assert.IsNotNull( audSrc );

		waveSpawnSound = Resources.Load<AudioClip>(
			"Sounds/Fox Warp In" );
		Assert.IsNotNull( waveSpawnSound );
	}

	void Update()
	{
		waveSpawn.Update( Time.deltaTime );
		if( waveCheck.Update( Time.deltaTime ) )
		{
			waveCheck.Reset();

			if( GameObject.FindGameObjectsWithTag(
				"SpaceFox" ).Length == 0/* ||
				waveSpawn.IsDone()*/ )
			{
				waveSpawn.Reset();

				for( int i = 0; i < curWaveSize; ++i )
				{
					var enemy = Instantiate( enemies[
						Random.Range( 0,enemies.Length )] );

					enemy.transform.position =
						GenerateRandPos();
				}

				audSrc.PlayOneShot( waveSpawnSound );

				curWaveSize = ( int )(
					( float )curWaveSize * waveIncrease );
			}
			if( GameObject.FindGameObjectWithTag(
				"SpaceKitten" ) == null )
			{
				var kitten = Instantiate( kittenPrefab );
				kitten.transform.position = GenerateRandPos();
			}
		}
	}

	Vector2 GenerateRandPos()
	{
		var rand = new Vector2( Random.Range( -1.0f,1.0f ),
			Random.Range( -1.0f,1.0f ) );
		if( rand.x == 0.0f ) rand.x = 1.0f;
		if( rand.y == 0.0f ) rand.y = 1.0f;
		return( rand.normalized * Random.Range(
			2f,maxSpawnRange ) );
	}

	[SerializeField] Timer waveSpawn = new Timer( 10.0f );
	[SerializeField] float maxSpawnRange = 0.0f;
	// [SerializeField] int kittensPerWave = 0;
	[SerializeField] GameObject kittenPrefab = null;
	[SerializeField] int waveSize = 0;
	[SerializeField] float waveIncrease = 0.0f;
	[SerializeField] GameObject[] enemies = {};

	Timer waveCheck = new Timer( 1.0f );
	int curWaveSize;

	AudioSource audSrc;

	AudioClip waveSpawnSound;
}
