using UnityEngine;
using System.Collections;

public class WorldGen : MonoBehaviour{

	Chunk chunk;

	// Use this for initialization
	void Start () 
	{
		//chunk = new Chunk();
		chunk = gameObject.GetComponent<Chunk>();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
