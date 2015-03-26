using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour 
	{
		void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.tag == "LevelStart")
			{
				other.gameObject.SetActive(false);
			    Application.LoadLevel("Main");
				Debug.Log("Load Level");
			}
		}
	}
