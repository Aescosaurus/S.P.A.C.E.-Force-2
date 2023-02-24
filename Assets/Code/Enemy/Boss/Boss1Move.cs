using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// movement mostly copied from PatrolFox
public class Boss1Move
	:
	EnemyBoss
{
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag( "Player" );

		StartCoroutine( WaitRegenPath() );
	}

	void Update()
	{
		if( activated )
		{
			if( curPoint > -1 )
			{
				var diff = curPath[curPoint] - ( Vector2 )transform.position;

				if( diff.sqrMagnitude < stopDist * stopDist ) TargetNextPoint();

				// todo: lerp don't set vel so we can still have knockback
				if( curPoint > -1 ) body.velocity = diff.normalized * moveSpd;
				else body.velocity = Vector2.zero;
			}
			else if( target != null )
			{
				var diff = target.transform.position - transform.position;

				body.velocity = diff.normalized * moveSpd;
			}
		}

		if( playerCheck.Update( Time.deltaTime ) )
		{
			playerCheck.Reset();

			if( target != null )
			{
				target = null;
				body.velocity = Vector2.zero;
				RegenPath();
			}
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
		if( curPath != null ) curPoint = 0;
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

	public override void ToggleActivate( bool active )
	{
		base.ToggleActivate( active );
		
		body.velocity = Vector2.zero;
	}

	Rigidbody2D body;
	GameObject player;

	List<Vector2> curPath;
	int curPoint = -1;
	int pathDir = 1;

	[SerializeField] float activateDelay = 0.1f;
	[SerializeField] float moveSpd = 1.0f;
	[SerializeField] float stopDist = 0.4f;
	
	[SerializeField] Timer playerCheck = new Timer( 0.2f );
	GameObject target = null;
}
