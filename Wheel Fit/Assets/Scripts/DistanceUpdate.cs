using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceUpdate : MonoBehaviour {

	Text txt;
	private int distance=1000;

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text="Distance Remaining : " + distance;
	}
	
	// Update is called once per frame
	void Update () {
		txt.text="Distance Remaining : " + distance;
		distance = PlayerPrefs.GetInt("DISTANCE"); 
		PlayerPrefs.SetInt("DISTANCE",distance); 
	}
}
