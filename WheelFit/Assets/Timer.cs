using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	//float startTime;
	float currentTime;
	[Range(0.0f,3600.0f)]
	public float timer = 3600;
	//public float timerIncrement;

	// Use this for initialization
	void Start () 
	{
		currentTime = 0;
		//startTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentTime += Time.unscaledDeltaTime;
		print (currentTime);
		
		if (currentTime >= timer) 
		{
			currentTime = 0;
			print ("You have been working hard. Have a break. HAVE A KIT-KAT!");
		}
	}

	void FixedUpdate()
	{

	}
}
