using UnityEngine;
using System.Collections;

using KinectNet20;
using TankGameInput;
using KinectForWheelchair;

public class StartCharController : MonoBehaviour {

	//used to set the games length
	public float GameLength;
	
	// Variables for output
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
			//throw new UnityException("Could not find a Kinect sensor.");
			Debug.Log("Could not find a Kinect sensor");
		}
		else // added this
		{
			// Enable the skeleton stream
			this.sensor = kinectSensors[0];
			this.sensor.SkeletonStream.Enable();
			if(!this.sensor.SkeletonStream.IsEnabled)
				throw new UnityException("Sensor could not be enabled.");
			
			// Create the input processor
			seatedInfoProcessor = new SeatedInfoProcessor();
			seatedInfo = null;
			return;
		}
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
		//if statement below stops crashes / freezes if no kinect is pluged in
		if(sensor != null) 
		{
			KinectControls();
		}
		KeyboardControls();
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
		
		Debug.Log (seatedInfo.Features.Position.y);
		
		//rotate the players position
		transform.Rotate(0, 1 * (seatedInfo.Features.Angle / (45)) * 60.0f * Time.deltaTime, 0);
		
		if(seatedInfo.Features.Position.y < 1.1f)
		{
			transform.Translate(Vector3.forward * 2.5f * Time.deltaTime);
			
		}
		else if(seatedInfo.Features.Position.y > 1.3f)
		{
			transform.Translate(Vector3.back * 2.5f * Time.deltaTime);
		}
	}
	void KeyboardControls()
	{
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0, 1 * 60.0f * Time.deltaTime, 0);
		}
		if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0, -1 * 60.0f * Time.deltaTime, 0);
			
		}
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.forward * 2.5f * Time.deltaTime);
		}
		
		else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.back * 2.5f * Time.deltaTime);
			
		}
	}
	//moved from pickup script
	void OnCollisionEnter(Collision other)
	{
		Debug.Log("Collision Detected" + other.collider.name);
		if(other.gameObject.tag == "item")
		{
			other.gameObject.SetActive(false);
			Debug.Log("item collected");
		}
		else if (other.gameObject.tag == "LevelStart")
		{		
			switch(other.collider.name)
			{
			case "GoSkiingDoor" :
				GameManager.GameTimeBegin = GameLength;
				GameManager.TimedGame = true;
				Debug.Log("SkiingGame Started");
				Application.LoadLevel("SkiingGame");
				break;
				
			case "GoSkiingEndlessDoor" :
				Debug.Log("SkiingGame Started");
				Application.LoadLevel("SkiingGame");
				break;
				
			case "GoRaftingDoor" :
				GameManager.GameTimeBegin = GameLength;
				GameManager.TimedGame = true;
				Debug.Log("WaterGame Started");
				Application.LoadLevel("WaterGame");
				break;
				
			case "GoRaftingEndlessDoor" :
				Debug.Log("WaterGame Started");
				Application.LoadLevel("WaterGame");
				break;				
			}
		}
	}
}
