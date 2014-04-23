using UnityEngine;
using System.Collections;

public class DieAfterXSeconds : MonoBehaviour 
{
	[SerializeField]
	private float lifeTime = 1f;

	private float startTime;

	void Start () 
	{
		startTime = Time.time;	
	}
	
	void Update () 
	{
		if (Time.time - startTime >= lifeTime)
		{
			Destroy(gameObject);
		}
	}
}
