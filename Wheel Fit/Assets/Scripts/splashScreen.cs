using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour
{
	void Start()
	{
		Invoke("StartGame",2.0f);
	}
	void StartGame()
	{
		Application.LoadLevel("game_start");
		Debug.Log("main game loaded");
	}

	/*
	float timeLeft = 5;

	void Update()
	{
		timeLeft -= Time.deltaTime;
		if(timeLeft <0)
		{
			Application.LoadLevel("game_start");
			Debug.Log("main game loaded");
		}
	}*/
	
}