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

		// healthBarPrefab = Resources.Load<GameObject>(
		// 	"Prefabs/HealthBarUI" );
		// Assert.IsNotNull( healthBarPrefab );

		// float y = gameObject.name == "Fox Boss 1" ? gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.y * 96 : gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.y * 48;
		// Vector3 sizeMultiplier = gameObject.name == "Fox Boss 1" ? new Vector3(2, 2, 2) : new Vector3(1, 1, 1);
		// screenOffset = new Vector3(0, y, 0);
		// totalHealth = health;
		// canvas = GameObject.Find("Canvas");		
		// Assert.IsNotNull( explodeSound );

		// explosionPrefab = Resources.Load<GameObject>(
		// 	"Prefabs/Explosion" );
		// Assert.IsNotNull( explosionPrefab );
		// 
		// audSrc = GetComponent<AudioSource>();

		//
		// Health Bar
		//
		// if (gameObject.tag != "Player")
		// {
		// 	healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, canvas.transform);
		// 	images = healthBar.GetComponentsInChildren<Image>();
		// 	foreach (Image img in images)
		// 	{
		// 		if (img.gameObject.tag == "HealthBarFill")
		// 		{
		// 			fillImage = img;
		// 		}
		// 	}
		// 	fillImage.fillAmount = 1;
		// 	healthBar.transform.localScale = sizeMultiplier;
		// }
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
			Destroy( gameObject );
		}
		else
		{
			var hpPercent = curHP / maxHP;

			var newScale = greenBar.localScale;
			newScale.x = hpPercent;
			greenBar.localScale = newScale;

			greenBar.localPosition = Vector3.left * ( hpPercent / 2.0f );
		}
		// health -= damage;
		// 
		// if( audSrc != null && hurtSound != null )
		// {
		// 	audSrc.PlayOneShot( hurtSound,0.6f );
		// }
		// 
		// if (gameObject.tag != "Player")
		// {
		// 	fillImage.fillAmount = ((float)health / (float)totalHealth);
		// }
		// 
		// if ( health <= 0 )
		// {
		// 	var explosion = Instantiate( explosionPrefab );
		// 	var clip = explosion.GetComponent<AudioSource>();
		// 	clip.clip = explodeSound;
		// 	clip.volume = 0.5f;
		// 	clip.Play();
		// 
		// 	Destroy( gameObject );
		// }
	}

	void UpdatePos()
	{
		hpBar.position = transform.position + Vector3.down * barOffset;
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
}