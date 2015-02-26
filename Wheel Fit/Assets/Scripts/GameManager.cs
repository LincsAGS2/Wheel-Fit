using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


    List<int> scores;
    public int playerFinalScore;
	public int courseLength;

	public float moveSpeed;
	// Use this for initialization
	void Start () {
        scores = new List<int>();
		for(int i = 1; i<11; i++)
		{
			string prefString = "DownHill" + courseLength.ToString() + i.ToString();
			Debug.Log(prefString);

			if(PlayerPrefs.GetInt(prefString) == 0)
			{
				PlayerPrefs.SetInt(prefString, 9999999);
			}
		
			scores.Add(PlayerPrefs.GetInt(prefString));

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetMoveSpeed()
	{
		return moveSpeed;
	}
	public void AdjustMoveSpeed(float amount)
	{
		moveSpeed += amount;
	}

    public void GameOver(int score)
    {
        scores.Add(score);
        scores.Sort();

		for(int i = 0; i<10; i++)
		{
			string prefString = "DownHill" + courseLength.ToString() + (i + 1).ToString();
			PlayerPrefs.SetInt(prefString, scores[i]);
		}
    }
}
