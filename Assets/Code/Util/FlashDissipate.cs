using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDissipate
	:
	MonoBehaviour
{
	void Start()
	{
		sprRend = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if( flashTime.Update( Time.deltaTime ) )
		{
			var col = sprRend.color;
			col.a = 1.0f - dissipateTime.GetPercent();
			sprRend.color = col;

			if( dissipateTime.Update( Time.deltaTime ) ) Destroy( gameObject );
		}
	}

	SpriteRenderer sprRend;

	[SerializeField] Timer flashTime = new Timer( 0.05f );
	[SerializeField] Timer dissipateTime = new Timer( 0.2f );
}
