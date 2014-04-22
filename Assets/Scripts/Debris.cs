using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour 
{
	void OnCollisionEnter()
	{
		audio.PlayOneShot (AudioClips.Collision);
	}
}
