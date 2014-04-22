using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public event System.Action<Health> KilledEvent;

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
	}

	public void Kill()
	{
		amount = 0f;

		if (KilledEvent != null)
		{
			KilledEvent(this);
		}
		
		respawner.StartRespawning();
	}
}
