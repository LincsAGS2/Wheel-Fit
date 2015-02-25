using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour {

	int width;
	int height;
	int depth;
	Voxel[,,] voxels;
	public GameObject preCube;
	public GameObject[] obstacles;
	public GameObject obstacle1;
	public GameObject obstacle2;
	public GameObject obstacle3;
	public GameObject obstacle4;
	float frnd;

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
					GameObject temp = Instantiate(preCube) as GameObject;
					Vector3 tempPos = new Vector3(i,j,k);
					temp.name = j.ToString();
					temp.transform.position = tempPos;

					if (j == 1)
					{
						temp.tag = "Obstacle";
					}

					if (temp.tag == "Obstacle")
					{
						if (frnd > 60.0f)
						{
							obstaclePos = new Vector3 (temp.transform.position.x + Random.value,2.5f,temp.transform.position.z + Random.value);
							Instantiate(obstacles[Random.Range(0, obstacles.Length)], obstaclePos, Quaternion.identity);
						}
					}
				}
			}
		}
	}
	// Use this for initialization
	void Start () 
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
