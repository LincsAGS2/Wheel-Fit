using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float rotateSpeed, rotateLimit, moveSpeed, leftrightMoveLimit, forwardbackMoveLimit, xMod, zMod;
	public GameManager gm;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void RotateRight()
	{
		//Rotates the object right until it hits the rotate limit
		transform.Rotate (new Vector3 (0, rotateSpeed, 0));
	
		if (Vector3.Angle(Vector3.forward, transform.forward) > rotateLimit)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, rotateLimit, transform.eulerAngles.z);
	}

	protected void RotateLeft()
	{
		//Rotates the object left until it hits the rotate limit
		transform.Rotate (new Vector3 (0, -rotateSpeed, 0));

		if (Vector3.Angle(Vector3.forward, transform.forward) > rotateLimit)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, -rotateLimit, transform.eulerAngles.z);
	}

	protected void MoveRight()
	{
		if (transform.position.x < leftrightMoveLimit + xMod) 
		{
			Vector3 tempPos = transform.position + new Vector3 (moveSpeed * 0.01f, 0, 0);
			transform.position = tempPos;
		}
	}

	protected void MoveLeft()
	{
		if (transform.position.x > -leftrightMoveLimit + xMod) 
		{
			Vector3 tempPos = transform.position + new Vector3 (-moveSpeed * 0.01f, 0, 0);
			transform.position = tempPos;
		}
	}

	protected void MoveForward()
	{
		if (transform.position.z < forwardbackMoveLimit + zMod) 
		{
			Vector3 tempPos = transform.position + new Vector3 (0, 0, moveSpeed * 0.01f);
			transform.position = tempPos;
		}
	}

	protected void MoveBack()
	{
		if (transform.position.z > -forwardbackMoveLimit + zMod) 
		{
			Vector3 tempPos = transform.position + new Vector3 (0, 0, -moveSpeed * 0.01f);
			transform.position = tempPos;
		}
	}

	void OnTriggerExit(Collider c)
	{
		try
		{
			Debug.Log("Hit Obstacle");
			gm.AdjustMoveSpeed (-0.5f);
		}
		catch
		{

		}
	}

	void MenuRight()
	{

	}

	void MenuLeft()
	{

	}

	void MenuSelect()
	{

	}

	void MenuUp()
	{

	}

	void MenuDown()
	{

	}

	/*protected void ResetAngle()
	{
		if (Vector3.Angle (Vector3.forward, transform.forward) < 0) 
		{
			transform.Rotate (new Vector3 (0, -rotateSpeed, 0));
		} 
		else if(Vector3.Angle(Vector3.forward, transform.forward) > 0)
		{
			transform.Rotate (new Vector3 (0, rotateSpeed, 0));
		}

	}*/
}
