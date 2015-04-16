using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScreenGUI : MonoBehaviour {

	[Header("Text Variables")]
	public Text myStatsText;
	public Text myCountDownText;

	[Header("Static Variables")]
	static float DistanceTravled; // these will need setting at end of game ( from timed mode )
	static float PlayersScore; // these will need setting at end of game ( from timed mode )
	static float TimePlayed; // these will need setting at end of game ( from timed mode )

	[Header("Time Variables")]
	public float StartTime;
	public float DisplayLength;

	// Use this for initialization
	void Start () 
	{
		StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (StartTime + DisplayLength <= Time.time)
		{
			Application.LoadLevel("game_start");
		}
		myStatsText.text = "You Traveled " + DistanceTravled + "Distance and scored" + PlayersScore + "points in" + TimePlayed + "Time";
		myCountDownText.text = "Returning to the menu in " + ((StartTime + DisplayLength)-Time.time).ToString("0") + " seconds";
	}
}
