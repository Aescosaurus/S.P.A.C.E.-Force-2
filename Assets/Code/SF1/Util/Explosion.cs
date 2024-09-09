using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Explosion
	:
	MonoBehaviour
{
	void Start()
	{
		audSrc = GetComponent<AudioSource>();
		Assert.IsNotNull( audSrc );
	}

	void Update()
	{
		if( !audSrc.isPlaying )
		{
			Destroy( gameObject );
		}
	}

	AudioSource audSrc;
}
