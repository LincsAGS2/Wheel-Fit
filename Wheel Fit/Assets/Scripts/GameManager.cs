using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    TextAsset highScores;
    List<int> scores;
    public int playerFinalScore;

	public float moveSpeed;
	// Use this for initialization
	void Start () {
        scores = new List<int>();
        highScores = Resources.Load("HighScores") as TextAsset;
        string[] tempScores = highScores.text.Trim('\n').Split('\n');
        tempScores = tempScores[0].Split(',');
        foreach (string s in tempScores)
        {
            scores.Add(int.Parse(s));
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

    }
}
