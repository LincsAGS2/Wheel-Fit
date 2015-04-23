using UnityEngine;
using System.Collections;

public class RespawnTimer : MonoBehaviour {
	
	[Header("NPC Variables")]
	public float NPCSpawnRate;
	public GameObject NPCSpawnerLocation;

	[Header("NPC Variables")]
	public float PowerUPSpawnRate;
	public GameObject PowerUPSpawnerLocation;
	
	[Header("Obstacle Variables")]
	public float ObstacleSpawnRate;
	public GameObject ObstacleSpawnerLocation;

	[Header("GameManager")]
	public GameManager gm;
	// Use this for initialization
	void Start ()
	{
		gm = FindObjectOfType<GameManager> ().GetComponent<GameManager> ();

		SpawnNPCInvoker();
		SpawnPowerUpsInvoker();
		SpawnObstaclesInvoker();
	}

	//NPC's
	void SpawnNPCInvoker()
	{
		SpawnNPC();
		Invoke("SpawnNPCInvoker", NPCSpawnRate / gm.moveSpeed);
		//Debug.Log(NPCSpawnRate / gm.moveSpeed);
	}
	void SpawnNPC()
	{
		NPCSpawnerLocation.GetComponent<NPCSpawner>().SpawnNPCs();
	}

	//Power ups
	void SpawnPowerUpsInvoker()
	{
		SpawnPowerUps();
		Invoke("SpawnPowerUpsInvoker",PowerUPSpawnRate / gm.moveSpeed);
		//Debug.Log("Current Power Up Spawn Rate " + PowerUPSpawnRate / gm.moveSpeed);	
	}
	void SpawnPowerUps()
	{
		PowerUPSpawnerLocation.GetComponent<PowerUpSpawner> ().SpawnPowerUps ();
	}

	//Obstacles
	void SpawnObstaclesInvoker()
	{
		SpawnObstacles();
		Invoke("SpawnObstaclesInvoker",ObstacleSpawnRate / gm.moveSpeed);
		//Debug.Log("Current Obstacle Spawn Rate " + ObstacleSpawnRate / gm.moveSpeed);
	}
	void SpawnObstacles()
	{
		ObstacleSpawnerLocation.GetComponent<ObsticleSpawner>().SpawnObstacles();
	}
}
