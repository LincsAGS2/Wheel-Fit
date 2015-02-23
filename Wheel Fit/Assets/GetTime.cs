using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetTime : MonoBehaviour {

	Text txt;
	string gameTime;
	string buffer;

	System.DateTime startTime;

	System.DateTime curTime;
	System.TimeSpan timeSinceStart;

	// Use this for initialization
	void Start () {
		startTime = System.DateTime.Now;
		txt = gameObject.GetComponent<Text>(); 

	//	curTime = System.DateTime.UtcNow.ToString("HH:mm");

			timeSinceStart = curTime - startTime;
		gameTime = timeSinceStart.Minutes + " : " + timeSinceStart.Seconds;
		txt.text = gameTime;

	//	txt.text= curTime;
	}
	
	// Update is called once per frame
	void Update () {
		curTime = System.DateTime.Now;
		timeSinceStart = curTime - startTime;
		gameTime = timeSinceStart.Minutes + " : " + timeSinceStart.Seconds;
		txt.text = gameTime;
	}
}
