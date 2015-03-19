using UnityEngine;
using System.Collections;

public class BasicBuoyancy: MonoBehaviour
{
	//Buoyancy Variables
	public GameObject Water;
	public float DownForce = 2;
	public float UpForce = 1;

	//Fake Buoyancy Variables
	public float CurrentYPosition;
	public bool Loop;

	// Use this for initialization
	void Start () 
	{
		CurrentYPosition = transform.position.y;
		Loop = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Buoyancy();//This Worked, but was to bouncy, may be usefull in the 
	FakeBuoyancy();
	}

	void FakeBuoyancy()
	{
		if (Loop == true) // if loop is true then the object it going up
		{			
			if (CurrentYPosition >= 0.25f)
			{
				Debug.Log ("entered");
				Loop = false;
			} 
			else 
			{
				CurrentYPosition += 0.005f; // adds 0.005f to the Y axis
			}
		} 
		else // if loop is falue then the object is going down
			if (CurrentYPosition <= -0.25f)
		{
			Loop = true;
		}
		else 
		{
			CurrentYPosition -= 0.005f;//removeds 0.005f to the Y axis
		}
		Vector3 newpos = new Vector3(transform.position.x,CurrentYPosition,transform.position.z); // creates a holder vec3
		transform.position = newpos; // updates the objects position to the new y position
	}

	void Buoyancy()
	{
		 
		//pushes the object up towards the water
		if(transform.position.y > Water.transform.position.y)
		{
		//dont really need this ad we are not dropping it to hard into the water
		//	rigidbody.AddForce(-Vector3.up * DownForce);
		}

		//pushes the object down into the water ( incase it bounced to far up )
		if(transform.position.y < Water.transform.position.y+1)
		{
			GetComponent<Rigidbody>().AddForce(Vector3.up * UpForce);				
		}
	}
}
