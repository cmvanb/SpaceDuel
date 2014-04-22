using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour 
{
	[SerializeField]
	private float chargeRate = 0.1f;

	[SerializeField]
	private GameObject laserShotPrefab;

	[SerializeField]
	private GameObject[] opponents;

	public bool IsCharging { get {return isCharging; } }
	private bool isCharging;

	private float chargeAmount;

	private Transform laserChargeObject;

	private Vector3 laserChargeOriginalScale;

	private int playerIndex;

	private Transform autoAimTarget;

	private Fuel fuelComponent;

	void Start()
	{
		laserChargeObject = transform.FindChild ("LaserCharge");

		laserChargeOriginalScale = laserChargeObject.localScale;

		laserChargeObject.gameObject.SetActive (false);

		playerIndex = GetComponent<PlayerController> ().PlayerIndex;

		autoAimTarget = opponents [0].transform;

		fuelComponent = GetComponent<Fuel> ();
	}

	public void Charge()
	{
		fuelComponent.Use (0.1f * Time.deltaTime);

		if (chargeAmount < 1f)
		{
			chargeAmount += chargeRate * Time.deltaTime;
		}

		if (!laserChargeObject.gameObject.activeSelf) 
		{
			laserChargeObject.gameObject.SetActive (true);
		}

		laserChargeObject.localScale = laserChargeOriginalScale * (0.25f + chargeAmount + Random.Range(-0.1f, 0.1f));

		isCharging = true;
	}

	public void Fire()
	{		
		AutoAimRaycast ();

		audio.PlayOneShot (AudioClips.LaserShot);

		Reset();
	}

	public void Reset()
	{
		chargeAmount = 0f;

		isCharging = false;

		laserChargeObject.gameObject.SetActive(false);
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
				healthComponent.Damage(chargeAmount);
			}
		}

		float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90f;

		InstantiateLaserShot(angle, hitDistance);
	}

	/*
	private void DoRaycast(bool shoot)
	{
		float angle = (180f - transform.eulerAngles.z) * Mathf.Deg2Rad;

		Vector3 direction = new Vector3 (Mathf.Sin (angle), Mathf.Cos (angle), 0f); 

		Ray ray = new Ray (transform.position, direction);

		RaycastHit raycastHit;

		int layerMask = ~(1 << (8 + playerIndex));

		bool hit = Physics.Raycast (ray, out raycastHit, 100f, layerMask);

		float hitDistance = 100f;

		if (hit)
		{
			Debug.DrawLine (transform.position, raycastHit.point, Color.red);

			hitDistance = raycastHit.distance;
		}
		else
		{
			Debug.DrawRay(transform.position, direction * 100f, Color.yellow);
		}

		if (shoot)
		{
			InstantiateLaserShot(transform.eulerAngles.z, hitDistance);
		}
	}
	*/

	private void InstantiateLaserShot(float angle, float distance)
	{
		GameObject laserShot = (GameObject)Instantiate (laserShotPrefab);

		laserShot.transform.position = transform.position;

		laserShot.transform.eulerAngles = new Vector3 (0f, 0f, angle);

		laserShot.transform.localScale = new Vector3 (laserShot.transform.localScale.x, distance, laserShot.transform.localScale.z);
	}
}
