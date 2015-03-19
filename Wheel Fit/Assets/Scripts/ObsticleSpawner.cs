using UnityEngine;
using System.Collections;

public class ObsticleSpawner : MonoBehaviour
{
	[Header("How many Obstacles spawn at a time")]
	public int SpawnAmount;
	[Header("Spawn range from gameobject X Z")]
	public float XAxisRange;
	public float ZAxisRange;
	[Header("Y axis spawn height")]
	public float SpawnYAxis;
	[Header("Minimum distance from obstacles")]
	public float MinimumDistance;
	[Header("How much it can fail before stoping")]
	public int FailCounter;
	[Header("Obstacles PreFabs")]
	public GameObject[] Obstacles;
	[Header("Debug")]
	public bool DisplaySpawnRange;

	public void SpawnObstacles ()
	{
		GameObject[]CurrentObsticles;
		float RayDistance = 100f;
		RaycastHit HitInfo;
		int FailCounter = 0;
		for (int i = 0; i < SpawnAmount; i++) {
			if(FailCounter >5) break;

			bool CanBuild = true;
			Vector3 SpawnLocation = new Vector3 (transform.position.x + UnityEngine.Random.Range (-XAxisRange, XAxisRange),
			                                     transform.position.y + SpawnYAxis,
			                                     transform.position.z + UnityEngine.Random.Range (-ZAxisRange, ZAxisRange)); 

			if (Physics.Raycast (SpawnLocation, Vector3.down, out HitInfo, RayDistance)) 
			{
				if (HitInfo.collider.tag == "Ground") 
				{
					CurrentObsticles = GameObject.FindGameObjectsWithTag ("Obstacle");

					foreach(GameObject ActiveObsticles in CurrentObsticles)
					{
						float temp = Vector3.Distance (HitInfo.point, ActiveObsticles.transform.position);
						if (temp <= MinimumDistance)
						{	
							CanBuild = false;
							i--;
							FailCounter++;
						}
					}

					if(CanBuild == true)
					{
						FailCounter = 0;
						GameObject tempObj = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], SpawnLocation, Quaternion.identity) as GameObject;
						tempObj.tag = "Obstacle"; // added this as before the ground was being tagged a obsticle insted of the .. obsticles
						tempObj.transform.SetParent(gameObject.transform);
					}					
				}	
				else //didnt hit tag "Ground"
				{
					i--;
					FailCounter++;
					Debug.Log ("Obsticle Spawner failed to hit the Ground");
				}
			}
			else// didnt hit anything
			{
				i--;
				FailCounter++;
				Debug.Log ("Obsticle Spawner failed to hit anything");
			}
		}
		CurrentObsticles = GameObject.FindGameObjectsWithTag ("Obstacle");
		Debug.Log(CurrentObsticles.Length + " out of " +  SpawnAmount + "Obstacles Spawned");
	}
	void Update()
	{
		if(DisplaySpawnRange == true)
		{
			//X axis
			Debug.DrawLine(transform.position,new Vector3 (transform.position.x + XAxisRange,transform.position.y,transform.position.z),Color.red,Time.deltaTime, false);
			Debug.DrawLine(transform.position,new Vector3 (transform.position.x + -XAxisRange,transform.position.y,transform.position.z),Color.red,Time.deltaTime, false);
			
			//Z Axis
			Debug.DrawLine(transform.position,new Vector3 (transform.position.x,transform.position.y,transform.position.z + ZAxisRange),Color.red,Time.deltaTime, false);
			Debug.DrawLine(transform.position,new Vector3 (transform.position.x,transform.position.y,transform.position.z + -ZAxisRange),Color.red,Time.deltaTime, false);			
		}
	}
}
