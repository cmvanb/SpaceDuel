using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour 
{
	void Start() 
	{
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Random.Range (0f, 360f));	
	}
}
