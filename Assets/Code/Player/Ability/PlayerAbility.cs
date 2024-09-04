using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility
	:
	MonoBehaviour
{
	protected virtual void Start()
	{
		cooldown.Finish();
	}

	protected virtual void Update()
	{
		cooldown.Update( Time.deltaTime );

		if( Input.GetAxis( "Ability" ) > 0.0f && cooldown.IsDone() )
		{
			ActivateAbility();

			cooldown.Reset();
		}
	}

	protected abstract void ActivateAbility();

	public void FinishCooldown()
	{
		cooldown.Finish();
	}

	[SerializeField] Timer cooldown = new Timer( 5.0f );
}
