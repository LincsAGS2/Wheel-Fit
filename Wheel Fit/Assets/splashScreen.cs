using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour
{
	static splashScreen instance;
	GameObject splashScreenImage;

	void Awake()
	{
		splashScreenImage = GameObject.Find("Image");
		if (instance)
		{
			Destroy(gameObject);
			hide();    
			return;
		}
		instance = this;    
		instance.splashScreenImage.SetActive(false);
		DontDestroyOnLoad(this);  
	}
	
	void Update()
	{
		if(!Application.isLoadingLevel)
			hide();
	}
	public static void show()
	{
		if (!InstanceExists()) 
		{
			return;
		}
		instance.splashScreenImage.SetActive(true);
	}
	public static void hide()
	{
		if (!InstanceExists()) 
		{
			return;
		}
		instance.splashScreenImage.SetActive(false);
	}
	static bool InstanceExists()
	{
		if (!instance)
		{
			return false;
		}
		return true;
		
	}
	
}