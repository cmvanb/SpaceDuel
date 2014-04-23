using UnityEngine;
using System.Collections;

public class AudioClips : MonoBehaviour 
{
	[SerializeField]
	private AudioClip laserShot;
	
	[SerializeField]
	private AudioClip collision;
	
	[SerializeField]
	private AudioClip accelerate;
	
	[SerializeField]
	private AudioClip death;

	public static AudioClip LaserShot;

	public static AudioClip Collision;
	
	public static AudioClip Accelerate;
	
	public static AudioClip Death;

	private static AudioClips instance;

	void Start()
	{
		LaserShot = laserShot;

		Collision = collision;
		
		Accelerate = accelerate;
		
		Death = death;

		instance = this;
	}

	public static void PlayOneShot(AudioClip clip)
	{
		instance.audio.PlayOneShot (clip);
	}
}
