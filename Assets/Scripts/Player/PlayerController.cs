using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	private float fuelUsageRate;

	[SerializeField]
	private int playerIndex = 0;
	public int PlayerIndex { get { return playerIndex; } }

	public int Kills { get; set; }

	private Vector3[] spawnPositions;

	private ParticleSystem rocketParticleEmitter;

	//public Fuel FuelComponent { get { return fuelComponent; } }
	//private Fuel fuelComponent;
	
	public Health HealthComponent { get { return healthComponent; } }
	private Health healthComponent;
	
	public SemiAutoLaserController LaserComponent { get { return laserComponent; } }
	private SemiAutoLaserController laserComponent;

	private float targetAngle;

	private string[] axisNames;

	private AudioSource engineSound;

	void Start()
	{
		rocketParticleEmitter = transform.FindChild("Emitter").GetComponent<ParticleSystem>();

		//fuelComponent = GetComponent<Fuel> ();
		
		healthComponent = GetComponent<Health> ();

		healthComponent.KilledEvent += OnKilled;

		laserComponent = GetComponent<SemiAutoLaserController> ();

		engineSound = transform.FindChild("Engine").GetComponent<AudioSource>();

		targetAngle = transform.eulerAngles.z;

		axisNames = new string[]
		{
			"Joy" + (playerIndex + 1).ToString () + "LeftX",
			"Joy" + (playerIndex + 1).ToString () + "LeftY",
			"Joy" + (playerIndex + 1).ToString () + "RightX",
			"Joy" + (playerIndex + 1).ToString () + "RightY",
			"Joy" + (playerIndex + 1).ToString () + "Triggers",
		};

		spawnPositions = new Vector3[]
		{
			transform.position,
		};
	}

	void Update () 
	{
		float xLook = Input.GetAxisRaw (axisNames[2]);
		float yLook = Input.GetAxisRaw (axisNames[3]);

		if (Mathf.Abs(xLook) > 0.01f
		    || Mathf.Abs(yLook) > 0.01f)
		{
			Vector3 lookPosition = transform.position + new Vector3 (xLook, -yLook, 0f);

			float angle = (Mathf.Atan2(transform.position.y - lookPosition.y, transform.position.x - lookPosition.x) * Mathf.Rad2Deg) - 90f;

			targetAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, 3f * Time.deltaTime);

			transform.eulerAngles = new Vector3 (0f, 0f, targetAngle);
		}

		float xMovement = Input.GetAxisRaw (axisNames[0]);
		float yMovement = Input.GetAxisRaw (axisNames[1]);

		if (Mathf.Abs(xMovement) > 0f
		    || Mathf.Abs(yMovement) > 0f)
		{
			Move (xMovement, yMovement);

			if (!engineSound.isPlaying)
			{
				engineSound.Play ();
			}
		}
		else
		{
			engineSound.Stop ();
		}

		float triggers = Input.GetAxisRaw (axisNames [4]);
		
		if (triggers < 0f)
		{
			laserComponent.Fire();
		}

		// original:
		/*
		if (triggers < 0f)
		{
			laserComponent.Charge();
		}
		else if (triggers == 0f)
		{
			if (laserComponent.IsCharging)
			{
				laserComponent.Fire();
			}
		}*/
	}

	void OnCollisionEnter()
	{
		audio.PlayOneShot (AudioClips.Collision);
	}

	public void Reset()
	{
		//fuelComponent.Reset();

		healthComponent.Reset();
		healthComponent.KilledEvent += OnKilled;

		laserComponent.Reset();

		targetAngle = transform.eulerAngles.z;
	}

	private void Move(float xMovement, float yMovement)
	{
		rigidbody.AddForce(new Vector3(xMovement, -yMovement, 0f) * Time.deltaTime * 100f);
		
		Vector3 moveDirection = transform.position + new Vector3 (xMovement, -yMovement, 0f);
		
		float moveAngle = 180f - (Mathf.Atan2(transform.position.y - moveDirection.y, transform.position.x - moveDirection.x) * Mathf.Rad2Deg);

		rocketParticleEmitter.transform.eulerAngles = new Vector3(moveAngle + 180f, 90f, 0f);
		rocketParticleEmitter.Emit(1);
	}

	private void OnKilled(int sourcePlayerIndex, Health sender)
	{
		for (int i = 0; i < laserComponent.Opponents.Length; ++i)
		{
			PlayerController opponentController = laserComponent.Opponents[i].GetComponent<PlayerController>();

			if (opponentController != null
			    && opponentController.PlayerIndex == sourcePlayerIndex)
			{
				opponentController.Kills++;
			}
		}

		AudioClips.PlayOneShot (AudioClips.Death);

		healthComponent.KilledEvent -= OnKilled;

		Debug.Log(name + " KILLED");

		gameObject.SetActive(false);

		healthComponent.Respawner.RespawnedEvent += OnRespawned;
	}

	private void OnRespawned()
	{
		healthComponent.Respawner.RespawnedEvent -= OnRespawned;

		gameObject.SetActive(true);

		Reset();

		transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)];
	}
}
