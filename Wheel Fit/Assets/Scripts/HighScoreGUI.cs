using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighScoreGUI : MonoBehaviour {

	public GameObject pageTitle;
	public List<GameObject> displayObjects;
	List<int> scores;
	public int courseLength;

	int[] courseLengthValues;
	int courseLengthIndex;

	// Use this for initialization
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		scores = new List<int> ();
		courseLengthValues = new int[] {10,25,50,100,200};
		courseLengthIndex = 0;
		courseLength = courseLengthValues [courseLengthIndex];
		DisplayScores(GetScores (courseLengthValues[courseLengthIndex]));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Gets the scores from player prefs.
	/// </summary>
	/// <returns>The scores.</returns>
	/// <param name="courseLengthIn">Course length in.</param>
	List<int> GetScores(int courseLengthIn)
	{
		List<int> tempScores = new List<int> ();
		for(int i = 1; i<11; i++)
		{
			string prefString = "DownHill" + courseLengthIn.ToString() + i.ToString();
			
			if(PlayerPrefs.GetInt(prefString) == 0)
			{
				PlayerPrefs.SetInt(prefString, 9999999);
			}
			
			tempScores.Add(PlayerPrefs.GetInt(prefString));
			
		}

		return tempScores;
	}

	/// <summary>
	/// Displaies the scores in the table.
	/// </summary>
	/// <param name="scoresIn">Scores in.</param>
	void DisplayScores(List<int> scoresIn)
	{
		pageTitle.GetComponent<Text> ().text = "Downhill Best Scores " + courseLengthValues [courseLengthIndex] + "0m";
		for(int i = 0; i<10; i++)
		{
			displayObjects[i].GetComponent<Text>().text = scoresIn[i].ToString();
		}
	}

	/// <summary>
	/// Nexts the high score table.
	/// </summary>
	public void NextTable()
	{
		if(courseLengthIndex < courseLengthValues.Length -1)
		{
			courseLengthIndex++;
		}
		else
		{
			courseLengthIndex = 0;
		}
		DisplayScores(GetScores (courseLengthValues[courseLengthIndex]));
	}


	/// <summary>
	/// Previouses the high score table.
	/// </summary>
	public void PreviousTable()
	{
		if(courseLengthIndex > 0)
		{
			courseLengthIndex--;
		}
		else
		{
			courseLengthIndex = courseLengthValues.Length - 1;
		}
		DisplayScores(GetScores (courseLengthValues[courseLengthIndex]));
	}
}
