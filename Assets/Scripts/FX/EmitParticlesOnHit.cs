using UnityEngine;
using System.Collections;

public class EmitParticlesOnHit : MonoBehaviour 
{
	[SerializeField]
	private GameObject particlePrefab;

	[SerializeField]
	private bool useNormal = true;

	public void Hit(RaycastHit raycastHit)
	{
		GameObject particle = (GameObject)Instantiate(particlePrefab);

		particle.transform.position = raycastHit.point;

		if (useNormal)
		{
			particle.transform.LookAt(raycastHit.point + raycastHit.normal);
		}
	}
}
