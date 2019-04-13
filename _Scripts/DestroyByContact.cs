using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;

	void Start()
	{
		// This instance of Hazard must find a GameObject with the GameController tag
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		// If this hazard succesfully found the GameController object, get the GameController script
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController> ();
		// If no script was found, display debug message
		if (gameController == null)
			Debug.Log ("Cannot find 'GameController' script reference");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy"))
			return;

		if (explosion != null)
			Instantiate (explosion, transform.position, transform.rotation);

		if (other.CompareTag ("Player"))
		{
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
		gameController.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
