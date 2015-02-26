using UnityEngine;
using System.Collections;

using KinectNet20;
using TankGameInput;
using KinectForWheelchair;

public class KinectInput : Controller {

	// Variables for output
	//public GameInputInfo InputInfo;
    public KinectForWheelchair.SeatedInfo seatedInfo;
    SeatedInfoProcessor seatedInfoProcessor;
	
	// Kinect stuff
	KinectSensor sensor;
	Skeleton[] skeletonData;
	
	// Input processor
	//InputProcessor inputProcessor;
	
	
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
            RotateRight();
        }
        else if(seatedInfo.Features.Angle < -5)
        {
            MoveLeft();
            RotateLeft();
        }
    }
}	