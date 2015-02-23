using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class LevelSpawner : MonoBehaviour {

	public GameObject chunkPrefab;

	public int 			levelLength;
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


	// Use this for initialization
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
	}



	// Update is called once per frame
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

	void endGame()
	{
		DateTime endTime = DateTime.Now;
		running = false;
		Debug.Log (startTime);
		Debug.Log (endTime);

		timeToComplete = Mathf.Round((float)(endTime - startTime).TotalSeconds);

		string gameOver = "Game Over!\nScore: " + timeToComplete.ToString ();
		gameOverText.enabled = true;
		gameOverText.text = gameOver;
	}
}
