using UnityEngine;
using System.Collections;

public class RandomPhysics : MonoBehaviour 
{
	[SerializeField]
	private Vector3 minForce;

	[SerializeField]
	private Vector3 maxForce;
	
	[SerializeField]
	private Vector3 minTorque;

	[SerializeField]
	private Vector3 maxTorque;

	void Start() 
	{
		Vector3 startForce = new Vector3 (Random.Range (minForce.x, maxForce.x), Random.Range (minForce.y, maxForce.y), Random.Range (minForce.z, maxForce.z));

		Vector3 startTorque = new Vector3 (Random.Range (minTorque.x, maxTorque.x), Random.Range (minTorque.y, maxTorque.y), Random.Range (minTorque.z, maxTorque.z));

		rigidbody.AddForce(startForce);

		rigidbody.AddTorque(startTorque);
	}
}
