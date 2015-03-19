using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour
{
	float timeLeft = 5;


	void Update()
	{
		timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 )
		{
			Application.LoadLevel("Main");
			Debug.Log ("Main Game Loaded");
		}
	}
	
}