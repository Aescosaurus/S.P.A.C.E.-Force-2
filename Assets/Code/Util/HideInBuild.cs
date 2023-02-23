using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInBuild
	:
	MonoBehaviour
{
	void Start()
	{
		if( !Application.isEditor )
		{
			GetComponentInChildren<SpriteRenderer>().enabled = false;
		}
	}
}
