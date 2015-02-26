using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {


	public void ChangeTo (string sceneName) {
		Application.LoadLevel (sceneName);
	}
}
