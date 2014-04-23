using UnityEngine;
using System.Collections;

public class SemiAutoLaserController : MonoBehaviour 
{
	[SerializeField]
	private float fireRate = 0.1f;
	
	[SerializeField]
	private GameObject laserShotPrefab;
	
	[SerializeField]
	private GameObject[] opponents;
	public GameObject[] Opponents { get { return opponents; } }

	public float ChargeAmount 
	{ 
		get 
		{ 
			if (Time.time - fireTimerStart >= fireRate)
			{
				return 1f;
			}

			return (Time.time - fireTimerStart) / fireRate; 
		} 
	}

	private int playerIndex;
	
	private Transform autoAimTarget;

	private float fireTimerStart;

	private bool chargeSound = true;

	void Start()
	{
		playerIndex = GetComponent<PlayerController> ().PlayerIndex;
		
		autoAimTarget = opponents [0].transform;

		Reset ();
	}

	void Update()
	{
		if (!chargeSound
		    && Time.time - fireTimerStart >= fireRate)
		{
			//audio.PlayOneShot(AudioClips.LaserReady);

			chargeSound = true;
		}
	}

	public void Fire()
	{
		if (Time.time - fireTimerStart < fireRate)
		{
			return;
		}

		fireTimerStart = Time.time;
		
		AutoAimRaycast ();
		
		audio.PlayOneShot(AudioClips.LaserShot);
		
		chargeSound = false;
	}
	
	public void Reset()
	{		
		fireTimerStart = Time.time - fireRate;

		chargeSound = true;
	}
	
	private void AutoAimRaycast()
	{
		Vector3 direction = autoAimTarget.position - transform.position;
		
		Ray ray = new Ray (transform.position, direction);
		
		RaycastHit raycastHit;
		
		int layerMask = ~(1 << (8 + playerIndex));
		
		bool hit = Physics.Raycast (ray, out raycastHit, 100f, layerMask);
		
		float hitDistance = 100f;
		
		if (hit)
		{
			hitDistance = raycastHit.distance;
			
			Health healthComponent = raycastHit.transform.GetComponent<Health>();
			
			if (healthComponent != null)
			{
				healthComponent.Damage(playerIndex, 1f);
			}
			
			EmitParticlesOnHit emitParticlesOnHit = raycastHit.transform.GetComponent<EmitParticlesOnHit>();
			
			if (emitParticlesOnHit != null)
			{
				emitParticlesOnHit.Hit(raycastHit);
			}
		}
		
		float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90f;
		
		InstantiateLaserShot(angle, hitDistance);
	}
	
	private void InstantiateLaserShot(float angle, float distance)
	{
		GameObject laserShot = (GameObject)Instantiate (laserShotPrefab);
		
		laserShot.transform.position = transform.position;
		
		laserShot.transform.eulerAngles = new Vector3 (0f, 0f, angle);
		
		laserShot.transform.localScale = new Vector3 (laserShot.transform.localScale.x, distance, laserShot.transform.localScale.z);

		laserShot.GetComponent<LaserShot> ().OwnerIndex = playerIndex;
	}
}
