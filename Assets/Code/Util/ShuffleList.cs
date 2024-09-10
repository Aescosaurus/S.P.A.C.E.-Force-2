using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShuffleList
{
	public static void Shuffle<T>( List<T> list )
	{
		int ind = list.Count;

		while( ind > 0 )
		{
			int curInd = ( int )Mathf.Floor( Random.Range( 0.0f,1.0f ) * ( float )ind );
			--ind;

			T temp = list[ind];
			list[ind] = list[curInd];
			list[curInd] = temp;
		}
	}
}
