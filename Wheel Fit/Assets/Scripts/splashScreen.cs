using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour
{

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel("Menu");
		}
	}
	
}