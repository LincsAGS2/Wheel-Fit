using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollisionController : MonoBehaviour {
	public GameManager gm;
	public GameObject CollisionEffect;

	public float SplashDuration = 1;
	public bool DisplayEffect = false;

	public Color CurrentColour;
	public Color StartColour;
	public Color EndColor = new Color(1,1,1,0.5f);

	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager> ().GetComponent<GameManager> ();

		CollisionEffect = GameObject.Find("SplashEffect Image");
		StartColour = CollisionEffect.GetComponent<Image>().color;
		CollisionEffect.SetActive(false); // hides the splash image untill needed
	}
	
	// Update is called once per frame
	void Update () {
		if(DisplayEffect == true)
		{
			SplashDuration -= Time.deltaTime;
			CollisionEffect.SetActive(true);

			if(SplashDuration < 0){	
				DisplayEffect = false;
				SplashDuration = 1;
				CollisionEffect.SetActive(false);
			}

			CurrentColour = CollisionEffect.GetComponent<Image>().color;
			CurrentColour = Color.Lerp(EndColor,StartColour,SplashDuration);
			CollisionEffect.GetComponent<Image>().color = CurrentColour;
		}


	}

	void OnTriggerEnter(Collider c) {
		if(c.collider.tag == "Obstacle")
		{
			gm.SetObstaclesHit(1);
			Destroy(c.gameObject);
			//stops the players speed going under 0.5f
			Debug.Log(gm.moveSpeed);
			Debug.Log (CurrentColour.a);
			DisplayEffect = true;

			if(gm.moveSpeed > 1.0f)
			{
				gm.AdjustMoveSpeed (-0.5f);
			}
		}

		if(c.collider.tag == "NPC")
		{
			if(gm.moveSpeed > 1.0f)
			{
				gm.AdjustMoveSpeed (-1.0f);
			}
		}

		if(c.collider.tag == "PowerUP")
		{
			if(c.collider.name == "Points(Clone)")
			{
				gm.AddPlayerScore(100);
				Destroy(c.gameObject);
			}
			if(c.collider.name == "Speed(Clone)")
			{
				gm.AdjustMoveSpeed(5);
				Destroy(c.gameObject);
			}
		}
	}
}
