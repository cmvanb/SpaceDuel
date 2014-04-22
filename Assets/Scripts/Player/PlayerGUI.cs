using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour 
{
	[SerializeField]
	private PlayerController playerComponent;

	[SerializeField]
	private GUISkin guiSkin;
	
	[SerializeField]
	private Texture fuelBarTexture;

	[SerializeField]
	private Texture fuelFullBarTexture;
	
	[SerializeField]
	private Texture healthBarTexture;
	
	[SerializeField]
	private Texture healthFullBarTexture;

	// rects
	private Rect boxRect;

	private Rect fuelRect;

	private Rect fuelFullBarRect;

	private Rect healthRect;
	
	private Rect healthFullBarRect;

	// player vars
	private int playerIndex;

	private string playerName;

	private float playerXOffset;

	void Start()
	{
		playerIndex = playerComponent.PlayerIndex;

		playerName = "Player " + (playerIndex + 1).ToString ();

		playerXOffset = playerIndex * 250f;

		boxRect = new Rect (100f + playerXOffset, Screen.height - 100f, 200f, 70f);

		fuelRect = new Rect (105f + playerXOffset, Screen.height - 78f, 100f, 70f);
		
		fuelFullBarRect = new Rect (150f + playerXOffset, Screen.height - 70f, 100f, 10f);

		healthRect = new Rect (105f + playerXOffset, Screen.height - 56f, 100f, 50f);
		
		healthFullBarRect = new Rect (150f + playerXOffset, Screen.height - 48f, 100f, 10f);
	}

	void OnGUI () 
	{
		GUI.skin = guiSkin;

		GUI.Box (boxRect, playerName);
		GUI.Label (fuelRect, "Fuel");
		GUI.Label (healthRect, "HP");
		
		GUI.DrawTexture (fuelFullBarRect, fuelFullBarTexture);
		GUI.DrawTexture (healthFullBarRect, healthFullBarTexture);

		Rect fuelBarRect = new Rect (150f + playerXOffset, Screen.height - 70f, 100f * playerComponent.FuelComponent.Amount, 10f);

		GUI.DrawTexture (fuelBarRect, fuelBarTexture);

		Rect healthBarRect = new Rect (150f + playerXOffset, Screen.height - 48f, 100f * playerComponent.HealthComponent.Amount, 10f);
		
		GUI.DrawTexture (healthBarRect, healthBarTexture);
	}
}
