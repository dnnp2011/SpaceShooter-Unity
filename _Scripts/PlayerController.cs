using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private float moveHorizontal, moveVertical, nextFire;
	private Vector3 movement;
	private AudioSource fireSound;

	public float speed, tilt, fireRate; 
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		fireSound = GetComponent<AudioSource> ();
	}

	void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			fireSound.Play ();
		}
	}

	void FixedUpdate()
	{
		moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");

		movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
		rb.position = new Vector3 
			(
				Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
				0,
				Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);

		rb.rotation = Quaternion.Euler (0f, 0f, rb.velocity.x * -tilt);
	}
}