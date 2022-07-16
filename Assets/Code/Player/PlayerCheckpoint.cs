using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheckpoint
	:
	MonoBehaviour
{
	public void ResetToLastCheckpoint()
	{
		if( lastCheckpoint != null )
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			transform.position = lastCheckpoint.transform.position;
		}
		else
		{
			SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
		}
	}

	public void ActivateCheckpoint( Checkpoint point )
	{
		lastCheckpoint?.ToggleActive( false );
		lastCheckpoint = point;
	}

	Checkpoint lastCheckpoint = null;
}
