using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CrystalChanger
	:
	MonoBehaviour
{
	void Start()
	{
#if UNITY_EDITOR && false
		int nEnabled = 0;
		foreach( var color in colorEnabled )
		{
			if( color ) ++nEnabled;
		}
		Assert.IsTrue( nEnabled >= 2 );

		Assert.IsTrue( colorEnabled[( int )startColor] );
#endif
		sprRend = GetComponentInChildren<SpriteRenderer>();

		hp = hitsToActivate;

		var crystals = GameObject.FindGameObjectsWithTag( "Crystal" );
		foreach( var crystal in crystals )
		{
			var shard = crystal.GetComponent<CrystalShard>();
			if( shard.crystalId == crystalId ) myCrystals.Add( shard );
		}
		
		curColor = startColor;
		if( !colorEnabled[( int )startColor] ) NextColor();
		else SetColor();
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.tag == "Bullet" )
		{
			Instantiate( changerFlashPrefab,sprRend.transform );

			if( --hp <= 0 )
			{
				NextColor();

				hp = hitsToActivate;
			}
		}
	}

	void NextColor()
	{
		int nextCol = ( int )curColor + 1;
		int resets = 2; // protection against infinite loop
		while( !colorEnabled[nextCol] )
		{
			++nextCol;
			if( nextCol >= ( int )CrystalShard.ShardColor.Count )
			{
				nextCol = 0;
				if( --resets <= 0 ) break;
			}
		}
		curColor = ( CrystalShard.ShardColor )nextCol;
		SetColor();
	}

	void SetColor()
	{
		foreach( var crystal in myCrystals ) crystal.OnColorChange( curColor );

		sprRend.sprite = crystalSprs[( int )curColor];
	}

	SpriteRenderer sprRend;

	List<CrystalShard> myCrystals = new List<CrystalShard>();

	[SerializeField] int hitsToActivate = 5;
	int hp;
	
	[SerializeField] List<Sprite> crystalSprs = new List<Sprite>();

	// crystalId used to track which crystals this changer can activate
	[SerializeField] int crystalId = 1;
	[SerializeField] List<bool> colorEnabled = new List<bool>();

	[SerializeField] CrystalShard.ShardColor startColor = CrystalShard.ShardColor.Blue;
	CrystalShard.ShardColor curColor;

	[SerializeField] GameObject changerFlashPrefab = null;
}
