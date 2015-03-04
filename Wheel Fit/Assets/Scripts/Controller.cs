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

	/// <summary>
	/// Rotates the character right.
	/// </summary>
	protected void RotateRight()
	{
		//Rotates the object right until it hits the rotate limit
		transform.Rotate (new Vector3 (0, rotateSpeed, 0));
	
		if (Vector3.Angle(Vector3.forward, transform.forward) > rotateLimit)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, rotateLimit, transform.eulerAngles.z);
	}

	/// <summary>
	/// Rotates the character left.
	/// </summary>
	protected void RotateLeft()
	{
		//Rotates the object left until it hits the rotate limit
		transform.Rotate (new Vector3 (0, -rotateSpeed, 0));

		if (Vector3.Angle(Vector3.forward, transform.forward) > rotateLimit)
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, -rotateLimit, transform.eulerAngles.z);
	}

	/// <summary>
	/// Moves the character right.
	/// </summary>
	protected void MoveRight()
	{
		if (transform.position.x < leftrightMoveLimit + xMod) 
		{
			Vector3 tempPos = transform.position + new Vector3 (moveSpeed * 0.01f, 0, 0);
			transform.position = tempPos;
		}
	}

	/// <summary>
	/// Moves the character left.
	/// </summary>
	protected void MoveLeft()
	{
		if (transform.position.x > -leftrightMoveLimit + xMod) 
		{
			Vector3 tempPos = transform.position + new Vector3 (-moveSpeed * 0.01f, 0, 0);
			transform.position = tempPos;
		}
	}

	/// <summary>
	/// Moves the character forward.
	/// </summary>
	protected void MoveForward()
	{
		if (transform.position.z < forwardbackMoveLimit + zMod) 
		{
			Vector3 tempPos = transform.position + new Vector3 (0, 0, moveSpeed * 0.01f);
			transform.position = tempPos;
		}
	}

	/// <summary>
	/// Moves the character back.
	/// </summary>
	protected void MoveBack()
	{
		if (transform.position.z > -forwardbackMoveLimit + zMod) 
		{
			Vector3 tempPos = transform.position + new Vector3 (0, 0, -moveSpeed * 0.01f);
			transform.position = tempPos;
		}
	}

	//added to fix collisions
	void OnTriggerEnter(Collider c) {
		if(c.collider.tag == "Obstacle")
		{
			Debug.Log("Hit Obstacle");
			//stops the players speed going under 0.5f
			if(gm.moveSpeed < 1.0f)
			{
			gm.AdjustMoveSpeed (-0.5f);
			}
			
		}
	}

	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="c">C.</param>
	void OnTriggerExit(Collider c)
	{
		try
		{
			//Debug.Log("Hit Obstacle");
			//gm.AdjustMoveSpeed (-0.5f);
		}
		catch
		{

		}
	}

	/// <summary>
	/// Moves menu selection right.
	/// </summary>
	void MenuRight()
	{

	}

	/// <summary>
	/// Moves menu selection left.
	/// </summary>
	void MenuLeft()
	{

	}

	 /// <summary>
	 /// Selects currently selected menu item.
	 /// </summary>
	void MenuSelect()
	{

	}

	/// <summary>
	/// Moves menu selection up.
	/// </summary>
	void MenuUp()
	{

	}

	/// <summary>
	/// Moves menu selection down.
	/// </summary>
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
