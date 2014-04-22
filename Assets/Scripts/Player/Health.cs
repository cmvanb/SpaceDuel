using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public event System.Action<Health> KilledEvent;

	public event System.Action<Health> RespawnedEvent;

	[SerializeField]
	private float defaultAmount;

	[SerializeField]
	private float respawnDelay = 1f;

	public float Amount { get { return amount; } }
	private float amount;

	private float respawnTimeStarted = -1f;

	private bool isRespawning = false;

	// Use this for initialization
	void Start () 
	{
		Reset ();
	}

	void Update()
	{
		if (isRespawning
			&& Time.time - respawnTimeStarted > respawnDelay)
		{
			StopRespawnTimer();

			Respawn();
		}
	}
	
	public void Damage(float amount)
	{
		this.amount -= amount;

		if (this.amount < 0f)
		{
			Kill();
		}
	}

	public void Reset()
	{
		amount = defaultAmount;

		StopRespawnTimer();
	}

	public void Kill()
	{
		amount = 0f;

		if (KilledEvent != null)
		{
			KilledEvent(this);
		}

		StartRespawnTimer();
	}

	public void Respawn()
	{
		Debug.Log("RESPAWN");

		if (RespawnedEvent != null)
		{
			RespawnedEvent(this);
		}
	}

	private void StartRespawnTimer()
	{
		Debug.Log("START RESPAWN");

		isRespawning = true;

		respawnTimeStarted = Time.time;
	}

	private void StopRespawnTimer()
	{
		isRespawning = false;

		respawnTimeStarted = -1f;
	}
}
