using UnityEngine;
using System.Collections;

using TankGameInput;

public class SoldierController : MonoBehaviour
{

	public InputController inputController;

	// Use this for initialization
	void Start ()
	{
		return;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Get the input info
		GameInputInfo inputInfo = this.inputController.InputInfo;

		// Set the soldier position
		if(inputInfo.SoldierInfo.IsSkeletonAvailable)
			this.transform.position = Vector3.right * 3f * inputInfo.SoldierInfo.Position;
		else
			this.transform.position = Vector3.zero;

		if(inputInfo.SoldierInfo.IsSkeletonAvailable)
			Debug.Log("Skeleton available.");

		return;
	}
}
