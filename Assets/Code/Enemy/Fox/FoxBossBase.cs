using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBossBase
	:
	MonoBehaviour
{
	public void Explode()
	{
		LevelHandler.SaveKitty();
	}
}
