using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour 
{
	
	void FixedUpdate () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "item")
		{
			other.gameObject.SetActive(false);
			Debug.Log("item collected");
		}
		else if (other.gameObject.tag == "LevelStart")
		{
				switch(other.collider.name)
			{
			case "GoSkiingDoor" :
				Application.LoadLevel("SkiingGame");
				break;
				
			case "GoSkiingEndlessDoor" :
				Application.LoadLevel("SkiingGame");
				break;
				
			case "GoRaftingDoor" :
				Application.LoadLevel("WaterGame");
				break;
				
			case "GoRaftingEndlessDoor" :
				Application.LoadLevel("WaterGame");
				break;

			}
		}
	}
}

