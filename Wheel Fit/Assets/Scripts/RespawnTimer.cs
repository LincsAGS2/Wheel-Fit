using UnityEngine;
using System.Collections;

public class RespawnTimer : MonoBehaviour {
	
	[Header("NPC Variables")]
	public float NPCSpawnRate;
	public GameObject NPCSpawnerLocation;

	[Header("Obstacle Variables")]
	public float ObstacleSpawnRate;
	public GameObject ObstacleSpawnerLocation;

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating("SpawnNPC",0,NPCSpawnRate);
		InvokeRepeating("SpawnObstacles",0,ObstacleSpawnRate);
	}
	
	void SpawnNPC()
	{
		NPCSpawnerLocation.GetComponent<NPCSpawner>().SpawnNPCs();
	}
	void SpawnObstacles()
	{
		ObstacleSpawnerLocation.GetComponent<ObsticleSpawner>().SpawnObstacles();
	}
}
