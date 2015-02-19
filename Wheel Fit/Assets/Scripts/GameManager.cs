using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public float moveSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetMoveSpeed()
	{
		return moveSpeed;
	}
	public void AdjustMoveSpeed(float amount)
	{
		moveSpeed += amount;
	}
}
