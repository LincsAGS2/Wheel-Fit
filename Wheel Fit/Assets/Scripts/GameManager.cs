using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	float PlayerScore;
	//List<int> scores;
	public int playerFinalScore;
	public int TimeLimit;
	
	public float moveSpeed;
	public int ObstaclesHit;

	public static bool TimedGame;
	public static float GameTimeBegin;
	public float StartTime;
	public float GameTimeLeft;
	// Use this for initialization
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		/*  scores = new List<int>();
		for(int i = 1; i<11; i++)
		{
			string prefString = "DownHill" + courseLength.ToString() + i.ToString();
			Debug.Log(prefString);

			if(PlayerPrefs.GetInt(prefString) == 0)
			{
				PlayerPrefs.SetInt(prefString, 9999999);
			}
		
			scores.Add(PlayerPrefs.GetInt(prefString));
			PlayerScore = PlayerPrefs.GetInt(prefString);

		}*/
		if (TimedGame == true)
		{
			StartTime = Time.time;
		}

		InvokeRepeating ("SetPlayerScore", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (TimedGame == true)
		{
			GameTimeLeft = GameTimeBegin - (Time.time - StartTime);
			if(GameTimeLeft <= 0)
			{
				Application.LoadLevel("game_start"); // change to leaderboard scene once created

			}
		}
	}

	public float GetTimeLeft()
	{
		return GameTimeLeft;
	}

	public float GetMoveSpeed()
	{
		return moveSpeed;
	}
	
	
	public float GetPlayerScore()
	{
		return PlayerScore;
	}
	
	public void SetPlayerScore()
	{
		PlayerScore += 10;
	}

	public void AddPlayerScore(int ScoreAdd)
	{
		PlayerScore += ScoreAdd;
	}
	public float GetObstaclesHit()
	{
		return ObstaclesHit;
	}
	public void SetObstaclesHit(int Num)
	{
		ObstaclesHit += Num;
	}
	/// <summary>
	/// Adjusts the move speed.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void AdjustMoveSpeed(float amount)
	{
		moveSpeed += amount;
	}
	/// <summary>
	/// Runs the game over functions.
	/// </summary>
	/// <param name="score">Score.</param>
	public void GameOver(int score)
	{
		/* scores.Add(score);
        scores.Sort();

		for(int i = 0; i<10; i++)
		{
			string prefString = "DownHill" + courseLength.ToString() + (i + 1).ToString();
			PlayerPrefs.SetInt(prefString, scores[i]);
		}*/
	}
}
