using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFox
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody2D>();

		StartCoroutine( WaitRegenPath() );
	}

	void Update()
	{
		if( curPoint > -1 )
		{
			var diff = curPath[curPoint] - ( Vector2 )transform.position;

			if( diff.sqrMagnitude < stopDist * stopDist ) TargetNextPoint();

			if( curPoint > -1 ) body.velocity = diff.normalized * moveSpd;
			else body.velocity = Vector2.zero;
		}
	}

	IEnumerator WaitRegenPath()
	{
		yield return( new WaitForSeconds( activateDelay ) );
		RegenPath();
	}

	void RegenPath()
	{
		curPath = PatrolManager.Get().GeneratePatrolPath( transform.position );
		curPoint = 0;
	}

	void TargetNextPoint()
	{
		if( curPath.Count == 1 ) curPoint = -1;
		else
		{
			curPoint += pathDir;
			if( curPoint >= curPath.Count )
			{
				pathDir = -1;
				curPoint = curPath.Count - 1;
			}
			else if( curPoint < 0 )
			{
				pathDir = 1;
				curPoint = 0;
			}
		}
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		// RegenPath();
	}

	Rigidbody2D body;

	List<Vector2> curPath;
	int curPoint = -1;
	int pathDir = 1;

	[SerializeField] float activateDelay = 0.1f;
	[SerializeField] float moveSpd = 1.0f;
	[SerializeField] float stopDist = 0.4f;
}
