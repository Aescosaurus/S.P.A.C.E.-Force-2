using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

// other.GetComponent<HealthBar>.Hurt( 1 );
public class HealthBar
	:
	MonoBehaviour
{
	void Start()
	{
		curHP = maxHP;

		hpBar = Instantiate( ResLoader.Load( "Prefabs/UI/HPBar" ) ).transform;
		UpdatePos();
		greenBar = hpBar.Find( "HPBarGreen" );

		hpBar.gameObject.SetActive( false );
		greenBar.gameObject.SetActive( false );

		hpBar.transform.localScale = barScale;
	}

	void LateUpdate()
	{
		UpdatePos();
	}

	void OnDestroy()
	{
		if( hpBar != null ) Destroy( hpBar?.gameObject );
	}

	public void Hurt( float damage )
	{
		curHP -= damage;
		if( curHP <= 0.0f )
		{
			PartHand.Get().SpawnParts( transform.position,explodePartType );
			if( tag == "Player" ) StartCoroutine( DestroyPlayer() );
			else Destroy( gameObject );
		}
		else
		{
			if( damage > 0.0f )
			{
				hpBar.gameObject.SetActive( true );
				greenBar.gameObject.SetActive( true );
			}

			var hpPercent = curHP / maxHP;

			var newScale = greenBar.localScale;
			newScale.x = hpPercent;
			greenBar.localScale = newScale;

			greenBar.localPosition = Vector3.left * ( ( 1.0f - hpPercent ) / 2.0f );
		}
	}

	void UpdatePos()
	{
		hpBar.position = transform.position + Vector3.down * barOffset;
	}

	IEnumerator DestroyPlayer()
	{
		var wep = GetComponentInChildren<PlayerWeapon>();
		wep.enabled = false;
		var spr = GetComponentInChildren<SpriteRenderer>();
		spr.enabled = false;
		var coll = GetComponentInChildren<Collider2D>();
		coll.enabled = false;

		yield return( new WaitForSeconds( 1.0f ) );

		wep.enabled = true;
		spr.enabled = true;
		coll.enabled = true;

		curHP = maxHP;
		hpBar.gameObject.SetActive( false );
		greenBar.gameObject.SetActive( false );
		GetComponent<PlayerCheckpoint>().ResetToLastCheckpoint();
	}

	public float GetHealth()
	{
		return( curHP );
	}

	Transform hpBar;
	Transform greenBar;

	[SerializeField] float maxHP = 1.0f;
	float curHP;
	
	[SerializeField] float barOffset = 0.6f;

	[SerializeField] Vector2 barScale = new Vector2( 1.0f,1.0f );

	[SerializeField] PartHand.PartType explodePartType = PartHand.PartType.Explode;
}