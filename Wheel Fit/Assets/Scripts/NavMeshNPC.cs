using UnityEngine;
using System.Collections;

public class NavMeshNPC : MonoBehaviour {

	NavMeshAgent MyNavAgent;
	public Vector3 TargetLocation;
	public Transform PlayerPosition; //the player
	public Vector3 StartLocation;
	public float NPCMaxSpeed;

	// Use this for initialization
	void Start ()
	{
		MyNavAgent = gameObject.GetComponent<NavMeshAgent>();
		MyNavAgent.speed = NPCMaxSpeed;
		StartLocation = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		// update the X target location to be infrount of the player
		PlayerPosition = GameObject.Find ("Player").transform;
		if (MyNavAgent.transform.position.x != PlayerPosition.position.x)
		{
			TargetLocation.x = PlayerPosition.position.x;
		}

		//both versions of the code below work, however using "MyNavAgent" due to being able to edit its propertices from other scripts
		//this would allow for future changes, such as changing the npc's speed ect at spawn
		//this.gameObject.GetComponent<NavMeshAgent>().destination = TargetLocation;
		MyNavAgent.SetDestination(TargetLocation);
		Debug.DrawLine(MyNavAgent.transform.position,TargetLocation,Color.red);
		Debug.DrawLine(StartLocation,TargetLocation,Color.blue); // from bot to target locationebug.DrawLine(MyNavAgent.transform.position,TargetLocation,Color.red); // from bot to target location
		Debug.DrawLine(MyNavAgent.transform.position,PlayerPosition.position,Color.green);//from bot to player location

		if(MyNavAgent.transform.position.z >= TargetLocation.z-0.5f) // incase the bot gets pushed fowards by another bot or there is a obsticle
		{
			Destroy(gameObject); // destroy the bot once it reaches its destination
		}
	}
	public void SetTargetLocation(Vector3 Target, float speed)
	{
		TargetLocation = Target;
		NPCMaxSpeed = speed;

	}
}
