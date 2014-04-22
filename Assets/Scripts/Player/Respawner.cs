using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{
	public event System.Action RespawnedEvent;

	[SerializeField]
	private float respawnDelay = 1f;

	private float respawnTimeStarted = -1f;

	private bool isRespawning = false;

	void Update ()
	{
		if (isRespawning
			&& Time.time - respawnTimeStarted > respawnDelay)
		{
			StopRespawnTimer();

			Respawn();
		}
	}

	public void StartRespawning()
	{
		Debug.Log("START RESPAWN");

		isRespawning = true;

		respawnTimeStarted = Time.time;
	}

	private void Respawn()
	{
		Debug.Log("RESPAWN");

		if (RespawnedEvent != null)
		{
			RespawnedEvent();
		}
	}

	private void StopRespawnTimer()
	{
		isRespawning = false;

		respawnTimeStarted = -1f;
	}
}
