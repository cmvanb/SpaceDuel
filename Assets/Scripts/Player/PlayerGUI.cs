using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour 
{
	[SerializeField]
	private PlayerController playerComponent;

	[SerializeField]
	private GUISkin guiSkin;
	
	[SerializeField]
	private Texture laserBarTexture;

	[SerializeField]
	private Texture laserFullBarTexture;
	
	[SerializeField]
	private Texture healthBarTexture;
	
	[SerializeField]
	private Texture healthFullBarTexture;

	// rects
	private Rect boxRect;

	private Rect laserRect;

	private Rect laserFullBarRect;

	private Rect healthRect;
	
	private Rect healthFullBarRect;

	private Rect killsRect;

	// player vars
	private int playerIndex;

	private string playerName;

	private float playerXOffset;

	void Start()
	{
		playerIndex = playerComponent.PlayerIndex;

		playerName = "Player " + (playerIndex + 1).ToString ();

		playerXOffset = playerIndex * 360f;

		boxRect = new Rect (100f + playerXOffset, Screen.height - 100f, 340f, 70f);

		laserRect = new Rect (115f + playerXOffset, Screen.height - 78f, 100f, 70f);
		
		laserFullBarRect = new Rect (180f + playerXOffset, Screen.height - 70f, 100f, 10f);

		healthRect = new Rect (115f + playerXOffset, Screen.height - 56f, 100f, 50f);
		
		healthFullBarRect = new Rect (180f + playerXOffset, Screen.height - 48f, 100f, 10f);

		killsRect = new Rect(300f + playerXOffset, Screen.height - 75f, 200f, 70f);
	}

	void OnGUI () 
	{
		GUI.skin = guiSkin;

		GUI.Box (boxRect, playerName);
		GUI.Label (laserRect, "Laser");
		GUI.Label (healthRect, "HP");

		GUIStyle killsStyle = new GUIStyle (guiSkin.label);

		killsStyle.fontSize = 32;

		GUI.Label (killsRect, playerComponent.Kills.ToString() + " Kills", killsStyle);
		
		GUI.DrawTexture (laserFullBarRect, laserFullBarTexture);
		GUI.DrawTexture (healthFullBarRect, healthFullBarTexture);

		Rect laserBarRect = new Rect (180f + playerXOffset, Screen.height - 70f, 100f * playerComponent.LaserComponent.ChargeAmount, 10f);

		GUI.DrawTexture (laserBarRect, laserBarTexture);

		Rect healthBarRect = new Rect (180f + playerXOffset, Screen.height - 48f, 100f * playerComponent.HealthComponent.Amount, 10f);
		
		GUI.DrawTexture (healthBarRect, healthBarTexture);
	}
}
