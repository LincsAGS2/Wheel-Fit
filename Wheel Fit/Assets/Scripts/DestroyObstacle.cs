using UnityEngine;
using System.Collections;

public class DestroyObstacle : MonoBehaviour 
{
	public float DestructionDistance; // how far behind the player the object needs to be
	public float UpdateTime;
	void Start()
	{
		//calling the deletion check every second as calling it in update would be a waste of resourses 
		InvokeRepeating("DestroyMe", 0, UpdateTime);
	}

	//to destroy the object
	void DestroyMe()
	{
		if(transform.position.z < GameObject.Find("Player").transform.position.z - DestructionDistance) 
		  {
			Destroy(gameObject);
		}
	}
}
