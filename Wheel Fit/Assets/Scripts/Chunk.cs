﻿using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour{

	int width;
	int height;
	int depth;
	//Voxel[,,] voxels;
	public GameObject preCube;
	public GameObject[] obstacles;
	public GameObject obstacle1;
	public GameObject obstacle2;
	public GameObject obstacle3;
	public GameObject obstacle4;
	public GameObject NPCSkiier;
	float frnd;

	GameObject temp;
	private float rnd;
	private Vector3 obstaclePos;
	/*public Chunk()
	{
		width = 10;
		height = 2;
		depth = 10;
		voxels = new Voxel[width, height, depth];
		preCube = new GameObject ();
		BuildChunk();
	}*/

	private void BuildChunk()
	{
		for (int i = 0; i < width; i++) 
		{
			for (int j = 0; j < height; j++)
			{
				for (int k = 0; k < depth; k++)
				{
					rnd = Random.Range (0.0F, 100.0F);
					frnd = (float)rnd;
					//voxels[i,j,k] = new Voxel(new Vector3(i,j,k));
					temp = Instantiate(preCube) as GameObject;
					temp.transform.SetParent(gameObject.transform);
					Vector3 tempPos = new Vector3(i,j,k);
					temp.name = j.ToString();
					temp.transform.position = tempPos;

<<<<<<< HEAD
					//commented out the below to fix the collisions
					//if (j == 1)
					//{
						//temp.tag = "Obstacle";
					//}

					//if (temp.tag == "Obstacle")
					//{
						if (frnd > 95.0f)
=======
						if (frnd > 98.0f)
>>>>>>> origin/Thomas_Sprint_2_Week_1_NPC_Obstacle
						{
							obstaclePos = new Vector3 (temp.transform.position.x + Random.value,2.5f,temp.transform.position.z + Random.value);
							GameObject tempObj = Instantiate(obstacles[Random.Range(0, obstacles.Length)], obstaclePos, Quaternion.identity) as GameObject;
							tempObj.tag = "Obstacle"; // added this as before the ground was being tagged a obsticle insted of the .. obsticles
							tempObj.transform.SetParent(gameObject.transform);
						}
<<<<<<< HEAD
					//}
=======
>>>>>>> origin/Thomas_Sprint_2_Week_1_NPC_Obstacle
				}
			}
		}
		//Maby add the chance for the NPC to spawn here???
		obstaclePos = new Vector3 (temp.transform.position.x + Random.value,1.5f,temp.transform.position.z + Random.value);
		GameObject tempNPC = Instantiate(NPCSkiier, obstaclePos, Quaternion.identity) as GameObject;
		tempNPC.tag = "NPC";
		tempNPC.transform.SetParent(gameObject.transform);
	}
	// Use this for initialization
	void Awake () 
	{
		width = 10;
		height = 2;
		depth = 10;
		obstacles = new GameObject[]{obstacle1,obstacle2,obstacle3,obstacle4};
		BuildChunk();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
