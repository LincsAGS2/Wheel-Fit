using UnityEngine;
using System.Collections;

public class ObsticleSpawner : MonoBehaviour
{
	public int SpawnAmount;
	public float XAxisSize;
	public float YAxisSize;
	public float SpawnZAxis;
	public float MinimumObsticleDistance;
	public int FailCounter;
	public GameObject[] Obstacles;
	void Start ()
	{
		GameObject[]CurrentObsticles;
		float RayDistance = 100f;
		RaycastHit HitInfo;
		int FailCounter = 0;
		for (int i = 0; i < SpawnAmount; i++) {
			if(FailCounter >5) break;

			bool CanBuild = true;
			Vector3 SpawnLocation = new Vector3 (UnityEngine.Random.Range (0, XAxisSize), SpawnZAxis, UnityEngine.Random.Range (0, YAxisSize));

			if (Physics.Raycast (SpawnLocation, Vector3.down, out HitInfo, RayDistance)) 
			{
				if (HitInfo.collider.tag == "Ground") 
				{
					CurrentObsticles = GameObject.FindGameObjectsWithTag ("Obstacle");

					foreach(GameObject ActiveObsticles in CurrentObsticles)
					{
						float temp = Vector3.Distance (HitInfo.point, ActiveObsticles.transform.position);
						if (temp <= MinimumObsticleDistance)
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
}
