using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFox
	:
	MonoBehaviour
{
	protected virtual void Start()
	{
		body = GetComponent<Rigidbody2D>();

		StartCoroutine( WaitRegenPath() );
	}

	protected virtual void Update()
	{
		if( curPoint > -1 )
		{
			var diff = curPath[curPoint] - ( Vector2 )transform.position;

			if( diff.sqrMagnitude < stopDist * stopDist ) TargetNextPoint();

			// todo: lerp don't set vel so we can still have knockback
			if( curPoint > -1 ) UpdateVel( diff.normalized * moveSpd );
			else UpdateVel( Vector2.zero );
		}
		else if( target != null )
		{
			var diff = target.transform.position - transform.position;

			UpdateVel( diff.normalized * moveSpd );
		}

		if( playerCheck.Update( Time.deltaTime ) )
		{
			playerCheck.Reset();

			if( PatrolManager.Get().CheckConnectedPlayer( transform.position ) )
			{
				curPoint = -1;
				target = PatrolManager.Get().GetPlayer();
			}
			else if( target != null )
			{
				target = null;
				UpdateVel( Vector2.zero );
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

	void OnCollisionEnter2D( Collision2D coll )
	{
		// RegenPath();
	}

	void UpdateVel( Vector2 newVel )
	{
		body.velocity = newVel;

		if( Mathf.Abs( newVel.x ) > 0.0f )
		{
			var newScale = transform.localScale;
			newScale.x = Mathf.Sign( newVel.x );
			transform.localScale = newScale;
		}
	}

	Rigidbody2D body;

	List<Vector2> curPath;
	int curPoint = -1;
	int pathDir = 1;

	[SerializeField] float activateDelay = 0.1f;
	[SerializeField] float moveSpd = 1.0f;
	[SerializeField] float stopDist = 0.4f;

	[SerializeField] Timer playerCheck = new Timer( 0.2f );
	GameObject target = null;
}
