using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour 
{
	public float GameLength;
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
				GameManager.GameTimeBegin = GameLength;
				GameManager.TimedGame = true;
				Application.LoadLevel("SkiingGame");
				break;
				
			case "GoSkiingEndlessDoor" :
				Application.LoadLevel("SkiingGame");
				break;
				
			case "GoRaftingDoor" :
				GameManager.GameTimeBegin = GameLength;
				GameManager.TimedGame = true;
				Application.LoadLevel("WaterGame");
				break;
				
			case "GoRaftingEndlessDoor" :
				Application.LoadLevel("WaterGame");
				break;

			}
		}
	}
}

