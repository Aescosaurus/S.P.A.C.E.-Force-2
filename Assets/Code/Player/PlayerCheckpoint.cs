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

			if( myAbility == null ) myAbility = GetComponentInChildren<PlayerAbility>();
			myAbility.FinishCooldown();
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

	PlayerAbility myAbility = null;

	Checkpoint lastCheckpoint = null;
}
