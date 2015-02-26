using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedUpdate : MonoBehaviour {

	Text txt;
	private int speed=1000;
	
	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text="Current Speed : " + speed;
	}
	
	// Update is called once per frame
	void Update () {
		txt.text="Current Speed : " + speed;
		speed = PlayerPrefs.GetInt("SPEED"); 
		PlayerPrefs.SetInt("SPEED",speed); 
	}
}
