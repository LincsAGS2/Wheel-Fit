using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour {
		
	public GameObject[] Obstacles;
	public GameObject[] Npcs;
	public GameObject[] PowerUps;

	public float ObstacleSpeed = 0.1f;
	public float NPCSpeed = 0.1f;
	public float PowerUPSpeed = 0.1f;
		
	void Update()
	{
		Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		Npcs = GameObject.FindGameObjectsWithTag("NPC");
		PowerUps = GameObject.FindGameObjectsWithTag("PowerUP");

		Debug.Log("Obstacles : " + Obstacles.Length + " Npcs : " + Npcs.Length + " PowerUps : " + PowerUps.Length);
		ObstacleUpdate ();
		NPCUpdate ();
		PowerUPUpdate ();
	}

	void ObstacleUpdate()
	{
		Vector3 CurrentPosition;
		for(int Counter = 0; Counter < Obstacles.Length; Counter++)
		{
			CurrentPosition = Obstacles[Counter].transform.position;
			Obstacles[Counter].transform.position = new Vector3 (CurrentPosition.x - 0, CurrentPosition.y - 0, CurrentPosition.z - ObstacleSpeed);
		}
	}

	void PowerUPUpdate()
	{
		Vector3 CurrentPosition;
		for(int Counter = 0; Counter < PowerUps.Length; Counter++)
		{
			CurrentPosition = PowerUps[Counter].transform.position;
			PowerUps[Counter].transform.position = new Vector3 (CurrentPosition.x - 0, CurrentPosition.y - 0, CurrentPosition.z - PowerUPSpeed);
		}
	}


	void NPCUpdate()
	{
		Vector3 CurrentPosition;
		for(int Counter = 0; Counter < Npcs.Length; Counter++)
		{
			CurrentPosition = Npcs[Counter].transform.position;
			Npcs[Counter].transform.position = new Vector3 (CurrentPosition.x - 0, CurrentPosition.y - 0, CurrentPosition.z - NPCSpeed);
		}
	}
}
