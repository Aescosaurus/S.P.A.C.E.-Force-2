using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitToMainMenu()
    {
        // SceneManager.LoadScene("Main Menu");
        LevelHandler.ExitToMainMenu();
    }

    public void RestartLevel()
    {
		// SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		LevelHandler.Reset();
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
