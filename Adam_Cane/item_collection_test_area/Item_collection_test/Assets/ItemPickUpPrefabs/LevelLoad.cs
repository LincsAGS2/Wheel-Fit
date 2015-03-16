using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour 
	{
		
		void FixedUpdate () 
		{
			
		}
		
		void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.tag == "LevelStart")
			{
				other.gameObject.SetActive(false);
			    Application.LoadLevel(1);
				Debug.Log("Load Level");
			}
		}
	}
