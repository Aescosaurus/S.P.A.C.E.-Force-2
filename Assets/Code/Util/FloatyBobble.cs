using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyBobble
	:
	MonoBehaviour
{
	void Start()
	{
		floatTop = transform.position.y + floatRange.Rand();
		floatBot = transform.position.y - floatRange.Rand();

		upSpd = floatSpd.Rand();
		downSpd = floatSpd.Rand();

		up = Random.Range( 0.0f,1.0f ) > 0.5f;
	}

	void Update()
	{
		var pos = transform.position;
		if( up )
		{
			pos.y += upSpd * Time.deltaTime;
			if( pos.y > floatTop )
			{
				pos.y = floatTop;
				up = false;
			}
		}
		else
		{
			pos.y -= downSpd * Time.deltaTime;
			if( pos.y < floatBot )
			{
				pos.y = floatBot;
				up = true;
			}
		}
		transform.position = pos;
	}

	[SerializeField] RangeF floatRange = new RangeF( 0.0f,0.2f );
	float floatTop;
	float floatBot;
	[SerializeField] RangeF floatSpd = new RangeF( 0.0f,0.1f );
	float upSpd;
	float downSpd;

	bool up;
}
