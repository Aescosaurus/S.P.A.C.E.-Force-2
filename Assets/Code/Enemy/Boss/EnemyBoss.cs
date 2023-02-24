using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss
	:
	MonoBehaviour
{
	public virtual void ToggleActivate( bool active )
	{
		activated = active;
	}

	public bool GetActivated()
	{
		return( activated );
	}

	protected bool activated = false;
}
