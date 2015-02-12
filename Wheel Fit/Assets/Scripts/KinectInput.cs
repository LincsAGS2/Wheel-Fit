using UnityEngine;
using System.Collections;

using KinectNet20;
using TankGameInput;

public class KinectInput : Controller {

	// Variables for output
	public GameInputInfo InputInfo;
	
	// Kinect stuff
	KinectSensor sensor;
	Skeleton[] skeletonData;
	
	// Input processor
	InputProcessor inputProcessor;
	
	
	// Use this for initialization
	void Start ()
	{
		// Find a Kinect sensor
		KinectSensorCollection kinectSensors = KinectSensor.KinectSensors;
		if(kinectSensors.Count == 0)
		{
			this.sensor = null;
			throw new UnityException("Could not find a Kinect sensor.");
		}
		
		// Enable the skeleton stream
		this.sensor = kinectSensors[0];
		this.sensor.SkeletonStream.Enable();
		if(!this.sensor.SkeletonStream.IsEnabled)
			throw new UnityException("Sensor could not be enabled.");
		
		// Create the input processor
		inputProcessor = new InputProcessor(this.sensor.CoordinateMapper, DepthImageFormat.Resolution320x240Fps30);
		this.InputInfo = inputProcessor.GameInputInfo;
		
		Debug.Log("Hello");
		return;
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
	
	// Update is called once per frame
	void Update ()
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
		this.InputInfo = inputProcessor.ProcessData(skeletonData);



		GameInputInfo inputInfo = InputInfo;



		// Set the soldier position
		if (inputInfo.SoldierInfo.IsSkeletonAvailable) 
		{
			if (inputInfo.SoldierInfo.Position > 0) 
			{
				MoveRight();
				RotateRight();
			}
			else if (inputInfo.SoldierInfo.Position < 0)
			{
				MoveLeft();
				RotateLeft();
			}
			Debug.Log (inputInfo.SoldierInfo.Position);
						//this.transform.position = Vector3.right * 3f * inputInfo.SoldierInfo.Position;
		}
		else
		{
			this.transform.position = Vector3.zero;
		}
		
		if(inputInfo.SoldierInfo.IsSkeletonAvailable)
			Debug.Log("Skeleton available.");


	}
}	