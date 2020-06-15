using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {

	public float score = 0;
	public GUISkin theSkin;
	public GUISkin theSkin2;
	private bool gooDie ;
	private int offset;
	// Use this for initialization
	void Start () {
		gooDie = false;
		offset = 100;
	}
	
	// Update is called once per frame
	void Update () {
		gooDie = GameObject.FindGameObjectWithTag ("Player").GetComponent<RobotController> ().goDie;
		if(gooDie == false)
			score += Time.deltaTime;

	}

	public void increaseScore(int amount)
	{
		score += amount;
	}

	void OnGUI()
	{
		GUI.skin = theSkin;

		if (gooDie == false) {
			GUI.Label ( new Rect (10, 10, 1000, 30), "Score : " + ((int)(score*10)) );
		} else {
			GUI.skin = theSkin2;
			GUI.Label ( new Rect (Screen.width/2-100, 100, 500, 500), "GAME OVER" );
			//GUI.skin = theSkin;
			if (score < 100) {
				offset = 80;
			} else if (score < 1000) {
				offset = 100;
			} else if (score < 10000) {
				offset = 100;
			} else if (score < 100000) {
				offset = 100;
			} else {
				offset = 110;
			}

			GUI.Label ( new Rect (Screen.width/2-offset, 150, 2000, 1000), "Score : " + ((int)(score*10)) );
		}

	}


}
