using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarSpawner
	:
	MonoBehaviour
{
	void Start()
	{
		starPrefab = Resources.Load<GameObject>(
			"Prefabs/Star" );
        mainMenuStarPrefab = Resources.Load<GameObject>("Prefabs/menuStar");

		for( int i = 0; i < nStars; ++i )
		{
			var rand = new Vector2( Random.Range( -1.0f,1.0f ),
				Random.Range( -1.0f,1.0f ) );
			if( rand.x == 0.0f ) rand.x = 1.0f;
			if( rand.y == 0.0f ) rand.y = 1.0f;
			var starPos = rand.normalized * Random.Range(
				Random.Range( 0.0f,starSpawnRange / 2.0f ),
				starSpawnRange );

			CreateStar( starPos );
		}
	}

	void CreateStar( Vector2 loc )
	{
        GameObject star;
		var spr = starSprites[Random.Range( 0,
			starSprites.Length )];
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Credits"))
        {
            star = Instantiate(mainMenuStarPrefab, transform);
        }
        else
        {
            star = Instantiate(starPrefab, transform);
        }
		star.transform.position = loc;
		star.GetComponent<SpriteRenderer>().sprite = spr;
	}

	GameObject starPrefab;
    GameObject mainMenuStarPrefab;

	[SerializeField] int nStars = 0;
	[SerializeField] float starSpawnRange = 0.0f;
	[Header( "Star Sprites" )]
	[SerializeField] Sprite[] starSprites = {};
}
