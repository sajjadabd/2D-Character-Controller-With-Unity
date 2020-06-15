using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;
	public Rigidbody2D myRigid ;
	public GUISkin theSkin;

	public bool goDie ;

	bool grounded = false;
	public Transform groundCheck;
	private float groundRadius = .2f;
	public LayerMask whatIsGround;

	public float jumpForce = 300f;

	public int jumpCounter = 0;

	private bool goman = false;
	private float move = 0f;

	//private float maxTime = 2f;
	//private float minSwipeDistance = 200;
	//private float startTime;
	//private float endTime;
	//private Vector3 startPosition;
	//private Vector3 endPosition;

	private AudioSource audioSource;

	Animator anim;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		anim = GetComponent<Animator> ();
		myRigid = GetComponent<Rigidbody2D>();
		Invoke ("goMan", 2f);
		goDie = false;

	}

	void goMan()
	{
		goman = true;
		move = 1f;
	}

	void OnGUI()
	{
		GUI.skin = theSkin;
		if (goman == false) {
			//if(GameObject.Find("MainCamera").GetComponent<ScoreScript>().score < 50)
				GUI.Label (new Rect(100,100,500,500),"Tap to Jump!!!");
		}

		if ( GUI.Button( new Rect (Screen.width/2-121/2,15,80,23), "RESET") )
		{
			SceneManager.LoadScene ("2DCharacterController");
		}
	}

	
	// Update is called once per frame
	void Update () {

		if (transform.position.y < -400) {

			SceneManager.LoadScene ("2DCharacterController");
			//Debug.Log ("Die");
			return;
			//audioSource.Stop ();
			//return;
		}

		//increase gravity scale
		if (Input.GetKeyDown (KeyCode.S)) {
			GetComponent<Rigidbody2D> ().gravityScale = 3f;
		}

		//for PC
		//( Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.Space) )


		if ( ( grounded || jumpCounter < 2) && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.Space))) {

			GetComponent<AudioSource> ().Play ();
			anim.SetBool ("Ground", false);
			//myRigid.AddForce (new Vector2 (0, jumpForce));

			if(jumpCounter == 0)
				myRigid.AddForce (new Vector2 (0, jumpForce));
			else if(jumpCounter == 1)
				myRigid.AddForce(new Vector2 (0,5f));

			jumpCounter++;
		}



		/*
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				startTime = Time.time;
				startPosition = touch.position;
			} else if (touch.phase == TouchPhase.Ended) {
				endTime = Time.time;
				endPosition = touch.position;
			}

			float swipeDistance = (startPosition - endPosition).magnitude;
			float swipeTime = startTime - endTime;

			if (swipeTime < maxTime && swipeDistance > minSwipeDistance) {
				swipe ();
			}
		}
		*/


		if (Input.touchCount > 0) {
			//Touch touch = Input.GetTouch(0);
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			//if(touch.phase == TouchPhase.Began)
			{
				//Debug.Log("Touch Began");
				if ( (grounded || jumpCounter < 2) ) {
					//if ( grounded && Input.GetTouch(0).phase == TouchPhase.Began) {	

					GetComponent<AudioSource> ().Play ();
					anim.SetBool ("Ground", false);
					//myRigid.AddForce (new Vector2 (0, jumpForce));

					if(jumpCounter == 0)
						myRigid.AddForce (new Vector2 (0, jumpForce));
					else if(jumpCounter == 1)
						myRigid.AddForce(new Vector2 (0,5f));

					jumpCounter++;

				}
			}
		}




	}

	/*
	void swipe()
	{
		Vector2 distance = endPosition - startPosition;
		if(Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
		{
			//Debug.Log("Horizontal Swipe");
			if(distance.x > 0)
			{
				//Debug.Log("Swipe Right");
				move = 1;
			}
			else if(distance.x < 0)
			{
				//Debug.Log("Swipe Left");
				move = -1;
			}
		}
		else if(Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
		{
			Debug.Log("Vertical Swipe");
			if(distance.y > 0)
			{
				//Debug.Log("Swipe Up");
			}
			else if(distance.y < 0)
			{
				//Debug.Log("Swipe Down");
			}
		}
	}
	*/

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", myRigid.velocity.y);


		if (grounded == true) {
			jumpCounter = 0;
			GetComponent<Rigidbody2D> ().gravityScale = 1f;
		}



		if (Input.GetKey (KeyCode.D)) {
			move = 1;

		} else if (Input.GetKey (KeyCode.A)) {
			move = -1;
		}
		//move = Input.GetAxis ("Horizontal");
		//Debug.Log ("move : " + move);

		anim.SetFloat ("Speed", Mathf.Abs(move));

		myRigid.velocity = new Vector2(move*maxSpeed , myRigid.velocity.y);


		if (move > 0 && facingRight == false ) {
			Flip ();

		} else if (move < 0 && facingRight == true )  {

			Flip ();
		}

	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "killer") {
			//Debug.Log ("Kill");
			goDie = true;
		}
	}
}
