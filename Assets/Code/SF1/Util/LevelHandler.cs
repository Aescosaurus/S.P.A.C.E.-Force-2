using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class LevelHandler
	:
	MonoBehaviour
{
	[System.Serializable]
	class SceneInfo
	{
		[SerializeField] public string sceneName = "";
		[SerializeField] public int kittiesToWin = 0;
	}

	// Move all instance data to static data.
	void Start()
	{
		foreach( var scene in sceneOrder )
		{
			sceneList.Add( scene );
		}

		sceneLoaderPrefab = Resources.Load<GameObject>(
			"Prefabs/NextSceneLoader" );
		Assert.IsNotNull( sceneLoaderPrefab );
		menuClickSound = Resources.Load<AudioClip>(
			"Sounds/Menu Click" );
		Assert.IsNotNull( menuClickSound );
		explosionPrefab = Resources.Load<GameObject>(
			"Prefabs/Explosion" );
		Assert.IsNotNull( explosionPrefab );
	}

	public static void SaveKitty()
	{
		++kittiesSaved;

		if( kittiesSaved >= kittiesToWin )
		{
			if( kittiesSaved <= 1 )
			{
				LoadNextScene();
			}
			else
			{
				Instantiate( sceneLoaderPrefab );
			}
		}
	}
	public static void DefeatBoss()
	{
		// TODO: Open upgrade menu.
		// When done upgrading go to next level.
	}
	public static void LoadNextScene()
	{
		++curLevel;

		SceneManager.LoadScene( sceneList[curLevel].sceneName );
		kittiesToWin = sceneList[curLevel].kittiesToWin;

		kittiesSaved = 0;

	}
	public static void Reset()
	{
		--curLevel;
		LoadNextScene();
	}
	public static void Restart()
	{
		curLevel = 0;
		LoadNextScene();
	}
    public static void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
    public static void ExitToMainMenu()
    {
        curLevel = -1;
        LoadNextScene();
    }
    public void PlayGame()
    {
		PlayClickSound();
        LoadNextScene();
    }
    public void ReturnToMain()
	{
		PlayClickSound();
		ExitToMainMenu();
    }
    public void ShowCredits()
	{
		PlayClickSound();
		LoadCreditsScene();
    }
    public void QuitGame()
	{
		PlayClickSound();
		Application.Quit();
    }
	static void PlayClickSound()
	{
		var expl = Instantiate( explosionPrefab );
		var audSrc = expl.GetComponent<AudioSource>();
		audSrc.clip = menuClickSound;
		audSrc.Play();
	}

	static GameObject sceneLoaderPrefab;
	static AudioClip menuClickSound;
	static GameObject explosionPrefab;

    [SerializeField] SceneInfo[] sceneOrder = {};

	static List<SceneInfo> sceneList = new List<SceneInfo>();

	static int curLevel = 0;
	static int kittiesSaved = -1;
	static int kittiesToWin = 0;
}
