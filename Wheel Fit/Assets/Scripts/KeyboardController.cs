using UnityEngine;
using System.Collections;

public class KeyboardController : Controller {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.A)) 
		{
			RotateLeft();
			MoveLeft();
		}
		else if(Input.GetKey(KeyCode.D))
		{
			RotateRight();
			MoveRight();
		}

		if(Input.GetKey(KeyCode.W))
		{
			MoveForward();
		}
		else if(Input.GetKey(KeyCode.S))
		{
			MoveBack();
		}

	
	}



}
