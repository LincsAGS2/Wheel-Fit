using UnityEngine;
using System.Collections;

using KinectNet20;
using TankGameInput;
using KinectForWheelchair;

public class kinectinput2 : Controller {
	
	// Variables for output
	//public GameInputInfo InputInfo;
	public KinectForWheelchair.SeatedInfo seatedInfo;
	SeatedInfoProcessor seatedInfoProcessor;
	
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
		seatedInfoProcessor = new SeatedInfoProcessor();
		seatedInfo = null;
		
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
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
            //RotateRight();
          //  this.transform.Rotate(0, 0.5f, 0);
        }
        if ( Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
            //RotateLeft();
          //  this.transform.Rotate(0, -0.5f, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Vector3 v = new Vector3(0.1f * Vector3.forward.x, 0.1f * Vector3.forward.y, 0.1f * Vector3.forward.z);
            this.transform.position += v;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Vector3 v = new Vector3(0.1f * Vector3.back.x, 0.1f * Vector3.back.y, 0.1f * Vector3.back.z);
            this.transform.position += v;
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
		
		if (seatedInfo.Features.Angle > 6)
		{
			MoveRight();
			//RotateRight();
		//	this.transform.Rotate(0, 0.5f, 0);
		}
		if(seatedInfo.Features.Angle < -6)
		{
			MoveLeft();
			//RotateLeft();
		//	this.transform.Rotate(0, -0.5f, 0);
		}
		if(seatedInfo.Features.Position.x < 0)
		{
            Vector3 v = new Vector3(0.1f * Vector3.forward.x, 0.1f * Vector3.forward.y , 0.1f * Vector3.forward.z);
			this.transform.position += v;
		}
		else if(seatedInfo.Features.Position.x > 5.0f)
		{
            Vector3 v = new Vector3(0.1f * Vector3.back.x, 0.1f * Vector3.back.y, 0.1f * Vector3.back.z);
            this.transform.position += v;
		}
     //   this.transform.position = new Vector3(seatedInfo.Features.Position.x, 0, seatedInfo.Features.Position.y) * 5;
    //    this.transform.forward = new Vector3(seatedInfo.Features.Direction.x, 0, seatedInfo.Features.Direction.y) * 5;
	}
}	