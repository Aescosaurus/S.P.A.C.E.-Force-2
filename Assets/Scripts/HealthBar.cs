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
		healthBarPrefab = Resources.Load<GameObject>(
			"Prefabs/HealthBarUI" );
		Assert.IsNotNull( healthBarPrefab );

        float y = gameObject.name == "Fox Boss 1" ? gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.y * 96 : gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.y * 48;
        Vector3 sizeMultiplier = gameObject.name == "Fox Boss 1" ? new Vector3(2, 2, 2) : new Vector3(1, 1, 1);
        screenOffset = new Vector3(0, y, 0);
        totalHealth = health;
        canvas = GameObject.Find("Canvas");        
		Assert.IsNotNull( explodeSound );

		explosionPrefab = Resources.Load<GameObject>(
			"Prefabs/Explosion" );
		Assert.IsNotNull( explosionPrefab );

		audSrc = GetComponent<AudioSource>();

        //
        // Health Bar
        //
        if (gameObject.tag != "Player")
        {
            healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, canvas.transform);
            images = healthBar.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                if (img.gameObject.tag == "HealthBarFill")
                {
                    fillImage = img;
                }
            }
            fillImage.fillAmount = 1;
            healthBar.transform.localScale = sizeMultiplier;
        }
    }

    private void LateUpdate()
    {
        if (gameObject.tag != "Player")
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position) + screenOffset;
        }
    }


    public void Hurt( int damage )
	{
		health -= damage;

		if( audSrc != null && hurtSound != null )
		{
			audSrc.PlayOneShot( hurtSound,0.6f );
		}

        if (gameObject.tag != "Player")
        {
            fillImage.fillAmount = ((float)health / (float)totalHealth);
        }

        if ( health <= 0 )
		{
			var explosion = Instantiate( explosionPrefab );
			var clip = explosion.GetComponent<AudioSource>();
			clip.clip = explodeSound;
			clip.volume = 0.5f;
			clip.Play();

			var scr = GetComponent<FoxBossBase>();
			if( scr != null ) scr.Explode();

			if( tag != "Player" )
			{
				DestroyHealthBar();
				Destroy( gameObject );
			}
		}
	}

	public int GetHealth()
	{
		return( health );
	}

    public void DestroyHealthBar()
    {
        Destroy(healthBar);
    }

    GameObject explosionPrefab;
    GameObject healthBar;
	AudioSource audSrc;
    Image[] images;
    Image fillImage;
    GameObject canvas;
    int totalHealth;
    Vector3 screenOffset;

	[SerializeField] int health = 0;
	[SerializeField] AudioClip hurtSound = null;
	[SerializeField] AudioClip explodeSound = null;
    /*[SerializeField]*/ GameObject healthBarPrefab = null;
}