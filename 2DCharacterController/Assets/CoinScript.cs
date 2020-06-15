using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

	public Transform coinEffect;
	public AudioClip clip;
	private AudioSource audioSource;

	ScoreScript sc;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("Boom");
		if (other.tag == "Player") {
			sc = GameObject.Find("MainCamera").GetComponent<ScoreScript>();
			//audioSource.Play ();
			//audio.Play();
			Instantiate(coinEffect,transform.position, Quaternion.identity);
			//Destroy (GameObject.Find ("CoinEffect(Clone)"),1f);
			GetComponent<AudioSource>().pitch = Random.Range(0.8f,1.2f);
			GetComponent<AudioSource>().volume = Random.Range(0.8f,1.2f);
			//GetComponent<AudioSource>().pitch = Random.Range(0.8f,1.2f);
			//GameObject<AudioSource>().pitch = Random.Range (0, 2);
			AudioSource.PlayClipAtPoint(clip,transform.position);

			//GetComponent<AudioSource>().Play();----------------this works too


			//audioSource.enabled = true;
			//audioSource.PlayOneShot (clip);
			Destroy (this.gameObject);
			sc.increaseScore (100);
		}

	}
}
