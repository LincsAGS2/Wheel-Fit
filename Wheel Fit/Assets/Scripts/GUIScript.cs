using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour {

	//GameManager ... can i removed this.. PLEASE??!!?
	public GameManager gm;
	public float mySpeed;
	public float myScore;
	public float myDistance;

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
		//Update Time
		curTime = System.DateTime.Now;
		timeSinceStart = curTime - startTime;
		gameTime = timeSinceStart.Minutes + " : " + timeSinceStart.Seconds;

		//Game Manager stuff...
		mySpeed = gm.GetMoveSpeed();
		myScore = gm.GetPlayerScore ();

		//update text
		ScoreText.GetComponent<Text>().text = "Score : " + myScore;
		TimeText.GetComponent<Text>().text = "Time : " + gameTime;
		DistanceText.GetComponent<Text>().text = "Distance : " + myDistance + " Mile";
		SpeedText.GetComponent<Text>().text = "Current Speed/sec : " + mySpeed;
		ObstaclesText.GetComponent<Text>().text = "Obstacles Hit : " + "";


	}

	void UpdateMydistance()
	{
		myDistance += gm.moveSpeed;
	}
}
