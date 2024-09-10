using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AsteroidSplit
	:
	MonoBehaviour
{
	void Start()
	{
		var nChildren = transform.childCount;
		for( int i = 1; i < nChildren; ++i ) spawnSpots.Add( transform.GetChild( i ) );
		
		Assert.IsTrue( nChildSpawns <= nChildren );
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.tag == "Bullet" )
		{
			if( --bulletsToBreak <= 0 ) BreakOpen( coll.gameObject );
		}
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.gameObject.tag == "Player" )
		{
			if( coll.gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude >
				Mathf.Pow( velocityToBreak,2 ) )
			{
				BreakOpen( coll.gameObject );
			}
		}
	}

	void BreakOpen( GameObject hitObj )
	{
		// make sure children don't collide with self
		GetComponent<Collider2D>().enabled = false;

		if( childAsteroidPrefab != null )
		{
			var randSpots = new List<int>();
			for( int i = 0; i < spawnSpots.Count; ++i ) randSpots.Add( i );
			ShuffleList.Shuffle( randSpots );

			for( int i = 0; i < Mathf.Min( nChildSpawns,spawnSpots.Count ); ++i )
			{
				var spawnPos = spawnSpots[randSpots[i]].position;
				var child = Instantiate( childAsteroidPrefab,spawnPos,
					Quaternion.Euler( 0.0f,0.0f,Random.Range( 0.0f,360.0f ) ) );

				// yeet children away with average of diff vectors
				var velDiffs = new List<Vector2>();
				velDiffs.Add( spawnPos - hitObj.transform.position );
				velDiffs.Add( spawnPos - transform.position );

				var totalVel = Vector2.zero;
				foreach( var diff in velDiffs ) totalVel += diff;
				totalVel /= velDiffs.Count;

				child.GetComponent<Rigidbody2D>().AddForce( totalVel * childVelMult,ForceMode2D.Impulse );
			}
		}

		// todo: particles!

		Destroy( gameObject );
	}

	List<Transform> spawnSpots = new List<Transform>();

	[SerializeField] int bulletsToBreak = 5;
	[SerializeField] float velocityToBreak = 9.0f;

	[SerializeField] GameObject childAsteroidPrefab = null;
	[SerializeField] int nChildSpawns = 2;
	[SerializeField] float childVelMult = 15.0f;
}
