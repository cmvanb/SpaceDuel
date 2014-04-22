using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour 
{
	[SerializeField]
	private float decayRate = 0.1f;

	private float lifeTimeScale;

	void Start()
	{
		lifeTimeScale = transform.localScale.x;
	}

	void Update () 
	{
		transform.localScale = new Vector3 (lifeTimeScale, transform.localScale.y, transform.localScale.z);

		if (lifeTimeScale > 0f)
		{
			lifeTimeScale -= decayRate * Time.deltaTime;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
