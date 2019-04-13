using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent <Rigidbody> ();

		// Random.insideUnitSphere returns a random Vector3 in order to randomize the spin
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
