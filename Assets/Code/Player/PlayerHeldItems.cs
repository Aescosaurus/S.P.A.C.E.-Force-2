using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeldItems
	:
	MonoBehaviour
{
	public void PickupKey( GameObject key )
	{
		keys.Add( key );
	}

	public bool TryConsumeKey()
	{
		if( keys.Count > 0 )
		{
			var usedKey = keys[Random.Range( 0,keys.Count )];
			PartHand.Get().SpawnParts( usedKey.transform.position );
			Destroy( usedKey );
			keys.Remove( usedKey );
			return( true );
		}
		return( false );
	}

	List<GameObject> keys = new List<GameObject>();
}
