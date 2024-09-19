using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PartHand
	:
	MonoBehaviour
{
	public enum PartType
	{
		Explode = 0,
		KeyExplode,
		ChickenExplode,
		AsteroidExplode,
		CrystalSpikeExplode,
		BulletHit,
		PlayerExplode
	}

	[System.Serializable]
	class ParticlePreset
	{
		[SerializeField] public GameObject prefab = null;
		[SerializeField] public RangeI amount = new RangeI( 25,35 );
	}

	void Start()
	{
		self = this;
	}

	public static PartHand Get()
	{
		Assert.IsNotNull( self );
		return( self );
	}

	public void SpawnParts( Vector2 pos,PartType type,RangeI amountOverride = null )
	{
		SpawnPartsDirectional( pos,Vector2.zero,type,amountOverride );
	}

	public void SpawnPartsDirectional( Vector2 pos,Vector2 dir,PartType type,RangeI amountOverride = null )
	{
		var preset = partPresets[( int )type];
		
		var curPartObj = GameObject.Instantiate( preset.prefab,pos,
			dir == Vector2.zero
			? Quaternion.identity
			: Quaternion.Euler( 0.0f,0.0f,Mathf.Atan2( dir.y,dir.x ) * Mathf.Rad2Deg - 90.0f ) );

		var partSys = curPartObj.GetComponent<ParticleSystem>();
		var nParts = ( amountOverride != null ? amountOverride.Rand() : preset.amount.Rand() );
		partSys.Emit( nParts );
		Destroy( curPartObj,partSys.main.duration );
	}

	static PartHand self = null;

	[SerializeField] List<ParticlePreset> partPresets = new List<ParticlePreset>();
}
