using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait, startWait, waveWait;
	public GUIText scoreText, restartText, gameOverText;

	private int score, hazardRange;
	private bool gameOver, restart;

	void Start()
	{
		hazardRange = Random.Range (hazardCount - (hazardCount / 2), hazardCount + (hazardCount / 2));
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		updateScore ();
		// In order to avoid pausing the whole game, but run the function as a co-routine
		StartCoroutine (spawnWaves ());
	}

	void Update()
	{
		hazardRange = Random.Range (hazardCount - (hazardCount / 2), hazardCount + (hazardCount / 2));
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);

			}
		}
	}

	IEnumerator spawnWaves()
	{
		// Must make return type IEnumerator, this line will make this routine WaitFor x Seconds before running, without pausing the whole game
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for(int i = 0; i < hazardRange; i++) 
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' to Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		updateScore ();
	}

	void updateScore()
	{
		scoreText.text = "Score: " + score.ToString ();
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}
