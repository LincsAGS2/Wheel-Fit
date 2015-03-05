using UnityEngine;
using System.Collections;

public class NPCSkiier : MonoBehaviour
{

	public Transform Target; //the player

	public float MoveSpeed = 2;
	public float rotateSpeed = 10;
	private int dIrection = -1;
	public float maxDistance = 2;
	public float directionDistance = 5;
	public float targetDistance = 5;
	public float followSpeed = 1;
	Vector3 BestPath = new Vector3 (0, 0, 0);
	bool sameDirection = false;
	// Use this for initialization
	void Start ()
	{
		Target = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{

		float Distance = Vector3.Distance (Target.position, transform.position);
		Debug.Log (Distance);
		Debug.DrawLine (Target.position, transform.position);

		Vector3 fwd = transform.TransformDirection (Vector3.forward);

		RaycastHit hit;
		float HighestHit = 0;

		//dodges obsticles infrount of him
		if (Physics.Raycast (transform.position, fwd, out hit, 2.5f)) 
		{
			Debug.DrawRay (transform.position, transform.forward, Color.green);

			if (sameDirection == false) 
			{
				//check to the right
				if (Physics.Raycast (transform.position, transform.right, out hit, 50.0f))
				{		
					Debug.DrawRay (transform.position, transform.right, Color.green);
					HighestHit = hit.distance;
					BestPath = transform.right;

				}
				//check to the left
				if (Physics.Raycast (transform.position, -transform.right, out hit, 50.0f)) 
				{
					Debug.DrawRay (transform.position, -transform.right, Color.green);
					if (hit.distance > HighestHit) {
						BestPath = -transform.right;
					}
				}
				sameDirection = true;
			}

			if(transform.position.x > 4 || transform.position.x < -4) // these are the map constraints so if the bot reaches teh edge it will need to move
			{
				sameDirection = false;
			}
			transform.Translate (BestPath * MoveSpeed * Time.smoothDeltaTime);
			if (Physics.Raycast (transform.position, transform.right, 1.0f) || Physics.Raycast (transform.position, -transform.right, 1.0f))
			{
				//check diagonals to ensure the bot is not on a corner

				//these are here just to draw
				//left side
				if (Physics.Raycast (transform.position, transform.forward - transform.right, 5.0f)) 
				{
					Debug.DrawRay (transform.position, transform.forward - transform.right, Color.blue);
				}
				//right side
				if (Physics.Raycast (transform.position, transform.forward + transform.right, 5.0f))
				{
					Debug.DrawRay (transform.position, transform.forward + transform.right, Color.blue);
				}
				if (!Physics.Raycast (transform.position, transform.forward - transform.right, 1.0f) && !Physics.Raycast (transform.position, transform.forward + transform.right, 1.0f))
				{
					sameDirection = false;
				}
			}

		} 
		else 
		{
			//attempts to get in the way of the player
			if (transform.position.x != Target.position.x)
			{
				if (transform.position.x < Target.position.x)
				{
					// move right
					transform.Translate (Vector3.right * followSpeed * Time.smoothDeltaTime);
				}
				else
				{
					//move left
					transform.Translate (Vector3.left * followSpeed * Time.smoothDeltaTime);
				}
			}

			//move fowards
			transform.Translate (Vector3.forward * MoveSpeed * Time.smoothDeltaTime);

			//look fowards
			transform.LookAt (new Vector3 (0, 0, 500));
		}

		/*
		// If distance is bigger then distance between target, wander around.
		if (Distance > targetDistance)
		{
			//Check if there is a collider in a certain distance of the object if not then do the following
			if(!Physics.Raycast(transform.position, transform.forward, maxDistance))
			{
				Debug.DrawLine(transform.position, transform.forward);
				// Move forward
				transform.Translate(Vector3.forward * MoveSpeed * Time.smoothDeltaTime);
			}
			else
			{
				// If there is a object at the right side of the object then give a random direction
				if(Physics.Raycast(transform.position, transform.right, directionDistance))
				{
					dIrection = Random.Range(-1, 2);
				}
				// If there is a object at the left side of the object then give a random direction
				if(Physics.Raycast(transform.position, -transform.right, directionDistance))
				{
					dIrection = Random.Range(-1, 2);
				}
				// rotate 90 degrees in the random direction 
				transform.Rotate(Vector3.up, 90 * rotateSpeed * Time.smoothDeltaTime * dIrection);
			}
		}
		// If current distance is smaller than the given ditance, then rotate towards player, and translate the rotation into forward motion times the given speed
		if (Distance < targetDistance)
		{
			transform.LookAt(Target);
			transform.Translate(Vector3.forward * followSpeed * Time.smoothDeltaTime);
		}
*/

	}
}
