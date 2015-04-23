using UnityEngine;
using System.Collections;

using KinectNet20;
using TankGameInput;
using KinectForWheelchair;

public class InGameControls : MonoBehaviour 
{
	/* Default settings
		6
		25
		4
	 */

	public GameManager gm;
	public float leftrightMoveLimit;
	public float SpeedLimit ;
	public float LeftRightMoveSpeed;
	public KinectForWheelchair.SeatedInfo seatedInfo;
	SeatedInfoProcessor seatedInfoProcessor;
	
	// Kinect stuff
	KinectSensor sensor;
	Skeleton[] skeletonData;

	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager> ().GetComponent<GameManager> ();

		// Find a Kinect sensor
		KinectSensorCollection kinectSensors = KinectSensor.KinectSensors;
		if(kinectSensors.Count == 0)
		{
			this.sensor = null;
			throw new UnityException("Could not find a Kinect sensor.");
		}
		else
		{
			// Enable the skeleton stream
			this.sensor = kinectSensors[0];
			this.sensor.SkeletonStream.Enable();
			if(!this.sensor.SkeletonStream.IsEnabled)
				throw new UnityException("Sensor could not be enabled.");
			
			// Create the input processor
			seatedInfoProcessor = new SeatedInfoProcessor();
			seatedInfo = null;
			
			Debug.Log("Hello");
			return;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(sensor != null) 
		{
			KinectControls();
		}
		KeyboardControls();
	}

	void OnApplicationQuit()
	{
		// Dispose the Kinect sensor
		if(sensor != null)
		{
			sensor.Dispose();
			sensor = null;
		}
		
		Debug.Log ("Goodbye");
		return;
	}

	void KeyboardControls()
	{
		//Left Right controls
		if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			MoveLeft();
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			MoveRight();
		}

		//Fowards Backwards Controls
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			MoveForward();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			MoveBack();
		}
	}
	void KinectControls()
	{
		// Retrieve skeleton data
		using(SkeletonFrame frame = this.sensor.SkeletonStream.OpenNextFrame(0))
		{
			if(frame != null)
			{
				// Allocate memory if needed
				if(skeletonData == null || skeletonData.Length != frame.SkeletonArrayLength)
				{
					skeletonData = new Skeleton[frame.SkeletonArrayLength];
				}
				
				frame.CopySkeletonDataTo(skeletonData);
			}
		}
		
		// Compute game input
		SeatedInfo[] seatedInfos = this.seatedInfoProcessor.ComputeSeatedInfos(skeletonData);
		
		int skeletonIndex = -1;
		for (int i=0; i<skeletonData.Length; i++)
		{
			if(seatedInfos[i].Posture == Posture.Seated)
				skeletonIndex = i;
		}
		
		// Compute seated info
		if (skeletonIndex != -1)
		{
			this.seatedInfo = seatedInfos[skeletonIndex];
			Debug.Log ("Tracking skeleton.");
		}
		
		
		if (seatedInfo == null) { return; };
		if (seatedInfo.Features == null) { return; };
		
		Debug.Log (seatedInfo.Features.Angle);
		
		if (seatedInfo.Features.Angle > 5)
		{
			MoveRight();
		}
		else if(seatedInfo.Features.Angle < -5)
		{
			MoveLeft();
		}

		// 0.2f Gap of no movement
		if(seatedInfo.Features.Position.y < 1.1f) // move fowards
		{
			MoveForward();
			
		}
		else if(seatedInfo.Features.Position.y > 1.3f)// move backwards
		{
			MoveBack();
		}
	}

	void MoveRight()
	{
		if (transform.position.x < leftrightMoveLimit) 
		{

			Vector3 tempPos = transform.position + new Vector3 ((LeftRightMoveSpeed + (gm.moveSpeed / 2))  * 0.01f, 0, 0);
			transform.position = tempPos;
		}
	}

	void MoveLeft()
	{
		if (transform.position.x > -leftrightMoveLimit) 
		{
			Vector3 tempPos = transform.position + new Vector3 ((-LeftRightMoveSpeed + -(gm.moveSpeed / 2)) * 0.01f, 0, 0);
			transform.position = tempPos;
		}
	}

	void MoveForward()
	{
		if (gm.moveSpeed < SpeedLimit) 
		{
			gm.AdjustMoveSpeed(1);
		}
	}

	void MoveBack()
	{
		if (gm.moveSpeed > 1) //stop it going below 1
		{
			gm.AdjustMoveSpeed(-1);
		}
	}
}