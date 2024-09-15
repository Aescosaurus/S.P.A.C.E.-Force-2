using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetPlatform
	:
	MonoBehaviour
{
	void Start()
	{
		sprRend = GetComponentInChildren<SpriteRenderer>();
		bounceDir = transform.Find( "BounceDir" );

		bounceReact.Finish();
	}

	void Update()
	{
		if( bounceReact.Update( Time.deltaTime ) && !setSprite )
		{
			sprRend.sprite = sprite;
			setSprite = true;
		}
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		sprRend.sprite = bounceSprite;
		bounceReact.Reset();
		setSprite = false;

		var body = coll.gameObject.GetComponent<Rigidbody2D>();
		if( body != null )
		{
			body.velocity = bounceDir.up * body.velocity.magnitude * bounceForceMult;
		}
	}

	SpriteRenderer sprRend;
	Transform bounceDir;

	[SerializeField] Sprite sprite = null;
	[SerializeField] Sprite bounceSprite = null;

	[SerializeField] Timer bounceReact = new Timer( 0.25f );
	[SerializeField] float bounceForceMult = 1.2f;

	bool setSprite = false;
}
