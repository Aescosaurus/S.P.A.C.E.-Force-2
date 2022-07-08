using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PartHand
	:
	MonoBehaviour
{
	void Start()
	{
		self = this;
	}

	public static PartHand Get()
	{
		Assert.IsNotNull( self );
		return( self );
	}

	public void SpawnParts( Vector2 pos )
	{
		var curPartObj = GameObject.Instantiate( partPrefab,pos,Quaternion.identity );

		var partSys = curPartObj.GetComponent<ParticleSystem>();
		partSys.Emit( amount.Rand() );
		Destroy( curPartObj,partSys.main.duration );
	}

	static PartHand self = null;

	[SerializeField] GameObject partPrefab = null;
	[SerializeField] RangeI amount = new RangeI( 25,35 );
}
