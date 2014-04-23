using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public event System.Action<int, Health> KilledEvent;

	[SerializeField]
	private GameObject respawnerPrefab;

	[SerializeField]
	private float defaultAmount;

	public float Amount { get { return amount; } }
	private float amount;

	public Respawner Respawner { get { return respawner; } }
	private Respawner respawner;

	// Use this for initialization
	void Start () 
	{
		respawner = ((GameObject)Instantiate(respawnerPrefab)).GetComponent<Respawner>();

		Reset ();
	}

	void Update()
	{
	}
	
	public void Damage(int sourcePlayerIndex, float amount)
	{
		this.amount -= amount;

		if (this.amount <= 0f)
		{
			Kill(sourcePlayerIndex);
		}
	}

	public void Reset()
	{
		amount = defaultAmount;
	}

	public void Kill(int sourcePlayerIndex)
	{
		amount = 0f;

		if (KilledEvent != null)
		{
			KilledEvent(sourcePlayerIndex, this);
		}
		
		respawner.StartRespawning();
	}
}
