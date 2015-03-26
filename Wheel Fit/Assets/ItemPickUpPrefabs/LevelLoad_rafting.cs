using UnityEngine;
using System.Collections;

public class LevelLoad_rafting : MonoBehaviour 

	{

		void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.tag == "LevelStart")
			{
				other.gameObject.SetActive(false);
				Application.LoadLevel("WaterGame");
				Debug.Log("Load Level");
			}
		}
	}
