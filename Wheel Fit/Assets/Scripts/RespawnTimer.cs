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

	public float MovementSpeed;

	public float test;
	// Use this for initialization
	void Start ()
	{
		//InvokeRepeating("SpawnNPC",0,NPCSpawnRate);
		//InvokeRepeating("SpawnPowerUps",0,PowerUPSpawnRate);
		//InvokeRepeating("SpawnObstacles",0,ObstacleSpawnRate);
		SpawnNPCInvoker();
		SpawnPowerUpsInvoker();
		SpawnObstaclesInvoker();
	}
	void Update()
	{
		MovementSpeed = gm.GetMoveSpeed()/5;
	}
	void SpawnNPCInvoker()
	{
		SpawnNPC();
		Invoke("SpawnNPCInvoker",NPCSpawnRate - MovementSpeed);
		PowerUPSpawnRate= 5;
	}
	void SpawnNPC()
	{
		NPCSpawnerLocation.GetComponent<NPCSpawner>().SpawnNPCs();
	}

	void SpawnPowerUpsInvoker()
	{
		float temp = (MovementSpeed /PowerUPSpawnRate ) *10;
		SpawnPowerUps();
		Invoke("SpawnPowerUpsInvoker",PowerUPSpawnRate);

	}
	void SpawnPowerUps()
	{
		PowerUPSpawnerLocation.GetComponent<PowerUpSpawner> ().SpawnPowerUps ();
	}

	void SpawnObstaclesInvoker()
	{
		float temp = (MovementSpeed /PowerUPSpawnRate ) *10;
		SpawnObstacles();
		Invoke("SpawnObstaclesInvoker",PowerUPSpawnRate);
	}
	void SpawnObstacles()
	{
		ObstacleSpawnerLocation.GetComponent<ObsticleSpawner>().SpawnObstacles();
	}
}
