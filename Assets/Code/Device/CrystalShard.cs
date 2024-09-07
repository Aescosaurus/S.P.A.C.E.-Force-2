using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CrystalShard
	:
	MonoBehaviour
{
	public enum ShardColor
	{
		Blue = 0,
		Green = 1,
		Yellow = 2,
		Count
	}

	void Awake()
	{
		Assert.IsTrue( myColor != ShardColor.Count );

		sprRend = GetComponentInChildren<SpriteRenderer>();
		sprRend.sprite = shardSprs[( int )myColor];
		coll = GetComponent<Collider2D>();
	}

	public void OnColorChange( ShardColor newColor )
	{
		bool colorMatch = ( newColor == myColor );
		
		coll.enabled = !colorMatch;
		sprRend.sprite = ( colorMatch ? hollowSprs : shardSprs )[( int )myColor];

		Instantiate( shardFlashPrefab,sprRend.transform );
	}

	SpriteRenderer sprRend;
	Collider2D coll;

	[SerializeField] List<Sprite> shardSprs = new List<Sprite>();
	[SerializeField] List<Sprite> hollowSprs = new List<Sprite>();
	[SerializeField] public int crystalId = 1;
	[SerializeField] ShardColor myColor = ShardColor.Blue;
	
	[SerializeField] GameObject shardFlashPrefab = null;
}
