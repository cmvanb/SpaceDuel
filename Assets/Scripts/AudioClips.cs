using UnityEngine;
using System.Collections;

public class AudioClips : MonoBehaviour 
{
	[SerializeField]
	private AudioClip laserShot;
	
	[SerializeField]
	private AudioClip collision;

	public static AudioClip LaserShot;

	public static AudioClip Collision;

	void Start()
	{
		LaserShot = laserShot;

		Collision = collision;
	}
}
