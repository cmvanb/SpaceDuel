using UnityEngine;
using System.Collections;

public static class RandomPosition 
{
	public static Vector3 Get()
	{
		return new Vector3 (Random.Range (Edges.LeftEdge, Edges.RightEdge), Random.Range (Edges.BottomEdge, Edges.TopEdge), 0f);
	}
}