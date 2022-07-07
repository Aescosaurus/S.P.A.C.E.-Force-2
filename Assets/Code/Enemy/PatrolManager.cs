using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PatrolManager
	:
	MonoBehaviour
{
	class PatrolPoint
	{
		public PatrolPoint( GameObject point )
		{
			this.point = point;
		}

		public GameObject point = null;
		public List<PatrolPoint> connected = new List<PatrolPoint>();
	}

	public static PatrolManager Get()
	{
		Assert.IsNotNull( self );
		return( self );
	}

	void Start()
	{
		self = this;
		player = GameObject.FindGameObjectWithTag( "Player" );

		patrolMask = LayerMask.GetMask( "Default" );

		var patrolPoints = GameObject.FindGameObjectsWithTag( "Patrol" );
		foreach( var point in patrolPoints )
		{
			points.Add( new PatrolPoint( point ) );
		}

		for( int i = 0; i < points.Count; ++i )
		{
			var curPoint = points[i];
			for( int j = i; j < points.Count; ++j )
			{
				var testPoint = points[j];
				if( CheckConnected( curPoint.point.transform.position,testPoint.point.transform.position ) )
				{
					curPoint.connected.Add( testPoint );
					testPoint.connected.Add( curPoint );
				}
			}
		}
	}

	// if point is within patrol range & has line of sight
	bool CheckConnected( Vector2 p1,Vector2 p2 )
	{
		var diff = p2 - p1;
		if( diff.sqrMagnitude > patrolRange * patrolRange ) return( false );

		var hit = Physics2D.Raycast( p1,diff,patrolRange,patrolMask );
		
		return( !hit );
	}

	public bool CheckConnectedPlayer( Vector2 pos )
	{
		return( CheckConnected( player.transform.position,pos ) );
	}

	public GameObject GetPlayer()
	{
		return( player );
	}

	// Generate path based on cur pos
	public List<Vector2> GeneratePatrolPath( Vector2 pos )
	{
		var nearest = new List<PatrolPoint>();
		foreach( var point in points )
		{
			if( CheckConnected( pos,point.point.transform.position ) ) nearest.Add( point );
		}

		PatrolPoint closest = null;
		float closestDist = 999999.0f;
		for( int i = 0; i < nearest.Count; ++i )
		{
			var dist = ( ( Vector2 )nearest[i].point.transform.position - pos ).sqrMagnitude;
			if( dist < closestDist )
			{
				closestDist = dist;
				closest = nearest[i];
			}
		}

		if( closest != null )
		{
			var patrolPath = new List<PatrolPoint>();
			patrolPath.Add( closest );
			for( int i = 0; i < maxPathSize; ++i )
			{
				foreach( var neigh in patrolPath[patrolPath.Count - 1].connected )
				{
					if( !patrolPath.Contains( neigh ) ) patrolPath.Add( neigh );
				}
			}

			var vec2Path = new List<Vector2>();
			foreach( var point in patrolPath ) vec2Path.Add( point.point.transform.position );
			return( vec2Path );
		}
		else return( null );
	}

	static PatrolManager self = null;
	GameObject player;

	LayerMask patrolMask;
	List<PatrolPoint> points = new List<PatrolPoint>();

	[SerializeField] float patrolRange = 20.0f;
	[SerializeField] float maxPathSize = 4;
}
