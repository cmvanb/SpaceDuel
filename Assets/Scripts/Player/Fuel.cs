using UnityEngine;
using System.Collections;

public class Fuel : MonoBehaviour 
{
	public float Amount { get { return amount; } }
	private float amount = 1f;

	public void Use(float amount)
	{
		if (this.amount >= amount)
		{
			this.amount -= amount;
		}
		else
		{
			this.amount = 0f;
		}
	}

	public void Reset()
	{
		amount = 1f;
	}
}
