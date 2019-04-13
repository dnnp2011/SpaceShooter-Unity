using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour 
{

	public Vector2 startWait, maneuverTime, maneuverWait;
	public float dodge, smoothing, tilt;
	public Boundary boundary;

	private float targetManeuver, currentSpeed;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}


	void Update () {

	}

	void FixedUpdate()
	{
		// Create a MoveTowards float value to represent a path the the Random X pos (targetManeuver) at a rate of Time.deltaTime * smoothing value
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		// Set velocity to a new Vector3 using the MoveTowards meneuver and taking into account the ships current speed
		rb.velocity = new Vector3 (newManeuver, 0f, currentSpeed);
		// Clamp the ship within the boundary values that mark the edges of the play area
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);
		// Set the rotation of the ship while turning as a Quaternion.Euler using the velocity * -tilt value specified
		rb.rotation = Quaternion.Euler (0f, 0f, rb.velocity.x * -tilt);

	}

	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

		while (true)
		{
			// -Mathf.Sign returns a -1 or 1, in this case to determine which direction to dodge, if the ship is on the negative side of the play area, it will dodge to the positive and vice versa
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range(maneuverWait.x, maneuverWait.y));
		}
	}
}