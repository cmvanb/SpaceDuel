using UnityEngine;
using System.Collections;

public class EdgeTeleporter : MonoBehaviour 
{
	private float edgeOffset = 0.5f;

	void Update() 
	{
		if (transform.position.x > Edges.RightEdge + edgeOffset)
		{
			transform.position = new Vector3(Edges.LeftEdge, -transform.position.y, transform.position.z);
		}

		if (transform.position.x < Edges.LeftEdge - edgeOffset)
		{
			transform.position = new Vector3(Edges.RightEdge, -transform.position.y, transform.position.z);
		}

		if (transform.position.y > Edges.TopEdge + edgeOffset)
		{
			transform.position = new Vector3(-transform.position.x, Edges.BottomEdge, transform.position.z);
		}

		if (transform.position.y < Edges.BottomEdge - edgeOffset)
		{
			transform.position = new Vector3(-transform.position.x, Edges.TopEdge, transform.position.z);
		}
	}
}
