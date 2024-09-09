using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NextSceneLoader
	:
	MonoBehaviour
{
	void Start()
	{
		audSrc = GetComponent<AudioSource>();
		Assert.IsNotNull( audSrc );
		audSrc.volume = 0.5f;

		GameObject.Find( "Music Player" )
			.GetComponent<AudioSource>().Stop();
		var player = GameObject.FindGameObjectWithTag( "Player" );
		player.GetComponent<BoxCollider2D>().enabled = false;
		player.GetComponent<EmancipatorShoot>().enabled = false;
	}

	void Update()
	{
		Time.timeScale = Vector2.Lerp( Vector2.right *
			Time.timeScale,Vector2.zero,
			timeSlowRate * Time.deltaTime ).x;

		if( !audSrc.isPlaying )
		{
			Time.timeScale = 1.0f;
			LevelHandler.LoadNextScene();
			Destroy( gameObject );
		}
	}

	AudioSource audSrc;

	[SerializeField] float timeSlowRate = 0.2f;
}
