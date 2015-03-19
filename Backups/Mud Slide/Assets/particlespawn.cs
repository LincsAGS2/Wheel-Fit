using UnityEngine;
using System.Collections;

public class particlespawn : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.transform.position.x < Camera.main.transform.position.x + 33
		    && this.transform.position.x > Camera.main.transform.position.x - 33
		    && this.transform.position.y < Camera.main.transform.position.y + 13
		    && this.transform.position.y > Camera.main.transform.position.y - 13)
		{
			particleSystem.Play ();
		} 
		else
		{
			particleSystem.Pause ();
		}
	}
}
