using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class die : MonoBehaviour {

	// Use this for initialization

	private bool goDie;
	void Start () {
		goDie = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			GetComponent<AudioSource> ().Play ();
			goDie = true;
			//StartCoroutine(Example());
			//SceneManager.LoadScene ("2DCharacterController");
			//Debug.Log ("Die");
			//return;
		}
	}

	void OnGUI()
	{
		if (goDie == true) {
			
		}
	}

	IEnumerator Example()
	{
		//print(Time.time);
		yield return new WaitForSeconds(5);
		//print(Time.time);
	}

}
