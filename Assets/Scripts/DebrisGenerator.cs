using UnityEngine;
using System.Collections.Generic;

public class DebrisGenerator : MonoBehaviour 
{
	[SerializeField]
	private List<GameObject> debrisPrefabs;

	[SerializeField]
	private int debrisCount;

	void Start () 
	{
		for (int i = 0; i < debrisCount; ++i)
		{
			InstantiateDebris();
		}
	}

	private GameObject InstantiateDebris()
	{
		int index = Random.Range(0, debrisPrefabs.Count);

		GameObject debrisObject = (GameObject)Instantiate(debrisPrefabs[index]);

		debrisObject.transform.position = RandomPosition.Get();

		return debrisObject;
	}
}
