using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour {
	
	Text txt;
	private int score=0;
	
	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text="Score : " + score;
	}
	
	// Update is called once per frame
	void Update () {
		txt.text="Score : " + score;
		score = PlayerPrefs.GetInt("SCORE"); 
		PlayerPrefs.SetInt("SCORE",score); 
	}
}
