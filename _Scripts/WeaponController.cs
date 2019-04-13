using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn; // Could make this a GameObject and then do shotSpawn.position or shotSpawn.rotation, but Unity will do this itself if marked as Transform
	public float fireRate, delay;

	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, fireRate); // Randomize fireRate
	}

	void Fire()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}
}
