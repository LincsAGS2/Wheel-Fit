using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class LevelSpawner : MonoBehaviour {

	public GameObject   chunkPrefab;
	public double 		timeToComplete;
	public Text 		gameOverText;
	public GameManager 	gm;
	public int 			maxMoveSpeed;
	GameObject 			temp;
	List<GameObject> 	chunks;
	Vector3 			chunkMoveVector, chunkResetVector;
	int 				chunkCount;
	float 				moveSpeed;
	bool 				running;
	DateTime 			startTime;
	int 				levelLength;


	// Use this for initialization
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {

		startTime = DateTime.Now;
		gm = FindObjectOfType<GameManager> ().GetComponent<GameManager>();
		running = true;

		chunks = new List<GameObject> ();
		for (int i = 0; i<7; i++) 
		{
			Vector3 positionAdjust = new Vector3(0,0, 10f*i);
			chunks.Add(Instantiate (chunkPrefab) as GameObject);
			chunks[i].transform.position += positionAdjust;
		}
		moveSpeed = gm.GetMoveSpeed ();
		chunkMoveVector = new Vector3 (0, 0, -moveSpeed / 100);
		chunkResetVector = new Vector3 (-5, 0, 59);
		levelLength = gm.courseLength;
	}



	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate () {
		if(running)
		{
			moveSpeed = gm.GetMoveSpeed();
			MoveChunks ();
			if(chunkCount >= levelLength)
			{
				endGame();
			}
		}

		if(Input.GetKey(KeyCode.UpArrow))
		{
			moveSpeed++;
			chunkMoveVector = new Vector3(0, 0, -moveSpeed / 100);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			moveSpeed--;
			chunkMoveVector = new Vector3(0, 0, -moveSpeed / 100);
		}
	
	}

	/// <summary>
	/// Moves the chunks.
	/// </summary>
	void MoveChunks()
	{

		foreach (GameObject g in chunks) 
		{
			if(g.transform.position.z <= -10)
			{
				g.transform.position = chunkResetVector;
				chunkCount++;
				if(gm.GetMoveSpeed() < maxMoveSpeed)
				gm.AdjustMoveSpeed(1);
			}
			chunkMoveVector = new Vector3 (0, 0, -moveSpeed / 100);
			g.transform.position += chunkMoveVector;

		}
	}

	///updates scoreboard
	void updateScoreboard(int time)
	{
		if(time > PlayerPrefs.GetInt ("Score1"))
		{
			PlayerPrefs.SetInt("Score9", PlayerPrefs.GetInt ("Score8"));
			PlayerPrefs.SetInt("Score8", PlayerPrefs.GetInt ("Score7"));
			PlayerPrefs.SetInt("Score7", PlayerPrefs.GetInt ("Score6"));
			PlayerPrefs.SetInt("Score6", PlayerPrefs.GetInt ("Score5"));
			PlayerPrefs.SetInt("Score5", PlayerPrefs.GetInt ("Score4"));
			PlayerPrefs.SetInt("Score4", PlayerPrefs.GetInt ("Score3"));
			PlayerPrefs.SetInt("Score3", PlayerPrefs.GetInt ("Score2"));
			PlayerPrefs.SetInt("Score2", PlayerPrefs.GetInt ("Score1"));
			PlayerPrefs.SetInt("Score1", timeToComplete);
		}
		else if(time > PlayerPrefs.GetInt ("Score2"))
		{
			PlayerPrefs.SetInt("Score9", PlayerPrefs.GetInt ("Score8"));
			PlayerPrefs.SetInt("Score8", PlayerPrefs.GetInt ("Score7"));
			PlayerPrefs.SetInt("Score7", PlayerPrefs.GetInt ("Score6"));
			PlayerPrefs.SetInt("Score6", PlayerPrefs.GetInt ("Score5"));
			PlayerPrefs.SetInt("Score5", PlayerPrefs.GetInt ("Score4"));
			PlayerPrefs.SetInt("Score4", PlayerPrefs.GetInt ("Score3"));
			PlayerPrefs.SetInt("Score3", PlayerPrefs.GetInt ("Score2"));
			PlayerPrefs.SetInt("Score2", time);
		}
		else if(time > PlayerPrefs.GetInt ("Score3"))
		{
			PlayerPrefs.SetInt("Score9", PlayerPrefs.GetInt ("Score8"));
			PlayerPrefs.SetInt("Score8", PlayerPrefs.GetInt ("Score7"));
			PlayerPrefs.SetInt("Score7", PlayerPrefs.GetInt ("Score6"));
			PlayerPrefs.SetInt("Score6", PlayerPrefs.GetInt ("Score5"));
			PlayerPrefs.SetInt("Score5", PlayerPrefs.GetInt ("Score4"));
			PlayerPrefs.SetInt("Score4", PlayerPrefs.GetInt ("Score3"));
			PlayerPrefs.SetInt("Score3", time);
		}
		else if(time > PlayerPrefs.GetInt ("Score4"))
		{
			PlayerPrefs.SetInt("Score9", PlayerPrefs.GetInt ("Score8"));
			PlayerPrefs.SetInt("Score8", PlayerPrefs.GetInt ("Score7"));
			PlayerPrefs.SetInt("Score7", PlayerPrefs.GetInt ("Score6"));
			PlayerPrefs.SetInt("Score6", PlayerPrefs.GetInt ("Score5"));
			PlayerPrefs.SetInt("Score5", PlayerPrefs.GetInt ("Score4"));
			PlayerPrefs.SetInt("Score4", time);
		}
		else if(time > PlayerPrefs.GetInt ("Score5"))
		{
			PlayerPrefs.SetInt("Score9", PlayerPrefs.GetInt ("Score8"));
			PlayerPrefs.SetInt("Score8", PlayerPrefs.GetInt ("Score7"));
			PlayerPrefs.SetInt("Score7", PlayerPrefs.GetInt ("Score6"));
			PlayerPrefs.SetInt("Score6", PlayerPrefs.GetInt ("Score5"));
			PlayerPrefs.SetInt("Score5", time);
		}
		else if(time > PlayerPrefs.GetInt ("Score6"))
		{
			PlayerPrefs.SetInt("Score9", PlayerPrefs.GetInt ("Score8"));
			PlayerPrefs.SetInt("Score8", PlayerPrefs.GetInt ("Score7"));
			PlayerPrefs.SetInt("Score7", PlayerPrefs.GetInt ("Score6"));
			PlayerPrefs.SetInt("Score6", time);
		}
		else if(time > PlayerPrefs.GetInt ("Score7"))
		{
			PlayerPrefs.SetInt("Score9", PlayerPrefs.GetInt ("Score8"));
			PlayerPrefs.SetInt("Score8", PlayerPrefs.GetInt ("Score7"));
			PlayerPrefs.SetInt("Score7", time);
		}
		else if(time > PlayerPrefs.GetInt ("Score8"))
		{
			PlayerPrefs.SetInt("Score9", PlayerPrefs.GetInt ("Score8"));
			PlayerPrefs.SetInt("Score8", time);
		}
		else if(time > PlayerPrefs.GetInt ("Score9"))
		{
			PlayerPrefs.SetInt("Score9", time);
		}
	}

	/// <summary>
	/// Ends the game.
	/// </summary>
	void endGame()
	{
		DateTime endTime = DateTime.Now;
		running = false;
		Debug.Log (startTime);
		Debug.Log (endTime);
		timeToComplete = Mathf.Round((float)(endTime - startTime).TotalSeconds);

		string gameOver = "Game Over!\nScore: " + timeToComplete.ToString ();

		updateScoreboard(timeToComplete);

		gameOverText.enabled = true;
		gameOverText.text = gameOver;
        gm.GameOver((int)timeToComplete);
	}
}
