using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner
	:
	MonoBehaviour
{
	void Update()
	{
		transform.Rotate( 0.0f,0.0f,spinSpd * Time.deltaTime );
	}

	[SerializeField] float spinSpd = 30.0f;
}
