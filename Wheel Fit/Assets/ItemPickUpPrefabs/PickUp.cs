﻿using UnityEngine;
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
	}
}

