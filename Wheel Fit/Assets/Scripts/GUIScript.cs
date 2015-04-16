using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour {

	//GameManager ... can i removed this.. PLEASE??!!?
	public GameManager gm;
	public float mySpeed;
	public float myScore;
	public float myDistance;
	public float MyObstaclesHit;

	public GameObject ScoreText;
	public GameObject TimeText;
	public GameObject DistanceText;
	public GameObject SpeedText;
	public GameObject ObstaclesText;


	//time stuff
	string gameTime;
	string buffer;
	System.DateTime startTime;
	System.DateTime curTime;
	System.TimeSpan timeSinceStart;

	// Use this for initialization
	void Start () {
		startTime = System.DateTime.Now;
		timeSinceStart = curTime - startTime;
		gameTime = timeSinceStart.Minutes + " : " + timeSinceStart.Seconds;

		InvokeRepeating ("UpdateMydistance", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.TimedGame != true)
		{
			//Update Time
			curTime = System.DateTime.Now;
			timeSinceStart = curTime - startTime;
			gameTime = timeSinceStart.Minutes + " : " + timeSinceStart.Seconds;

			TimeText.GetComponent<Text>().text = "Time : " + gameTime;
		}
		else
		{
			TimeText.GetComponent<Text>().text = "Time Reamining : " + gm.GetTimeLeft().ToString("0");
		}

		//Game Manager stuff...
		mySpeed = gm.GetMoveSpeed();
		myScore = gm.GetPlayerScore ();
		MyObstaclesHit = gm.GetObstaclesHit();

		//update text
		ScoreText.GetComponent<Text>().text = "Score : " + myScore;
		DistanceText.GetComponent<Text>().text = "Distance : " + myDistance + " Metre";
		SpeedText.GetComponent<Text>().text = "Current Speed/sec : " + mySpeed;
		ObstaclesText.GetComponent<Text>().text = "Obstacles Hit : " + MyObstaclesHit;


	}

	void UpdateMydistance()
	{
		myDistance += gm.moveSpeed;
	}
}
