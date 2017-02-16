using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerMove : MonoBehaviour {

	public GameObject player;

//	public GameObject playerShadow;

	Rigidbody2D rb;

	int jumpCounter;

	bool playerIsFlying;

	float moveSpeed;

	public bool screenTransition;

//	float shadowMoveAmount = 0.5f;
//	float shadowMoveTimer = 0;

	public bool facingLeft;
	public bool facingRight;

	bool playerIsAirborn;

	bool playerInvulnerable;

	int health;

	//restarting bools

	bool playerHasFlight;
	bool playerCanDoubleJump;
	bool playerCanMoveRegular;

	public GameObject startingPlatform1;
	public GameObject startingPlatform2;

	public GameObject slowZone1;
	public GameObject slowZone2;
	public GameObject slowZone3;
	public GameObject slowZone4;

//	public GameObject healthText;

	//Timer Stuff
	public GameObject gameTimer;
	private float gameTime = 21f;
	private float timerFlash = 0.0f;

	bool timerCountdown;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();

		jumpCounter = 0;

		playerIsFlying = false;

		facingLeft = false;
		facingRight = true;

		playerIsAirborn = false;

		playerInvulnerable = false;

		screenTransition = false;

		moveSpeed = 25f;

		health = 10;

		playerHasFlight = true;
		playerCanDoubleJump = true;
		playerCanMoveRegular = true;

		startingPlatform1.SetActive (false);
		startingPlatform2.SetActive (false);

		slowZone1.SetActive (false);
		slowZone2.SetActive (false);
		slowZone3.SetActive (false);
		slowZone4.SetActive (false);

		gameTimer.SetActive (false);

		timerCountdown = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 playerPos = new Vector3 (player.transform.position.x,
			player.transform.position.y,
			player.transform.position.z);

//		Vector3 shadowPos = new Vector3 (playerShadow.transform.position.x,
//			playerShadow.transform.position.y,
//			playerShadow.transform.position.z);

		if (screenTransition == false) {

			if (Input.GetKey (KeyCode.A)) {
				playerPos.x -= moveSpeed * Time.deltaTime;
				facingLeft = true;
				facingRight = false;
			}

			if (Input.GetKey (KeyCode.D)) {
				playerPos.x += moveSpeed * Time.deltaTime;
				facingLeft = false;
				facingRight = true;
			}
		}

//		if (playerHasShadow == true) {
//			if (facingLeft == true) {
//				shadowPos.x = playerPos.x + 3.5f;
//			}
//
//			if (facingRight == true) {
//				shadowPos.x = playerPos.x - 3.5f;
//			}
//		}

//		shadowPos.y -= shadowMoveAmount * Time.deltaTime;
//
//		shadowMoveTimer += Time.deltaTime;
//
//		if(shadowMoveTimer >= 1) {
//			shadowMoveAmount = -shadowMoveAmount;
//			shadowMoveTimer = 0;
//		}

		if (screenTransition == false) {

			if (Input.GetKeyDown (KeyCode.Space) && playerIsFlying == false && playerCanDoubleJump == true) {

				jumpCounter++;

				if (jumpCounter <= 2) {

					rb.velocity = new Vector3 (0, 75, 0);
				}
			}

			if (Input.GetKeyDown (KeyCode.Space) && playerIsFlying == false && playerCanDoubleJump == false) {
				jumpCounter++;

				if (jumpCounter <= 1) {

					rb.velocity = new Vector3 (0, 75, 0);
				}
			}

			if (Input.GetKey (KeyCode.Space)) {
				playerIsAirborn = true;
			}

//			if (playerHasShadow == true) {
//				if (playerIsAirborn == true) {
//					shadowPos.y = playerPos.y + 0.5f;
//				}
//			}

		}

		//screen transitions
		if (screenTransition == true && facingRight == true) {
			playerPos.x += 10 * Time.deltaTime;
		}

		if(screenTransition == true && facingLeft == true) {
			playerPos.x -= 10 * Time.deltaTime;
		}

//		playerShadow.transform.position = shadowPos;

		player.transform.position = playerPos;

		//health and dying
//		Text playerHealthText = healthText.GetComponent<Text>();
//		playerHealthText.text = "Health: " + health;
//
//		if (health <= 0) {
//			Application.LoadLevel ("Dead");
//		}

		Text timeMeter = gameTimer.GetComponent<Text>();
		timeMeter.text = "Time: " + (int)gameTime;

		timerFlash += Time.deltaTime*5;


		if((int)timerFlash%2==1){
			timeMeter.color = new Color (1f, 0f, 0f);
		}

		if((int)timerFlash%2==0){
			timeMeter.color = new Color (1f, 1f, 1f);
		}

		if (timerCountdown == true) {
			gameTime -= Time.deltaTime;
		}

		if (gameTime < 1) {
			Application.LoadLevel ("Dead");
		}
	
	}

	void FixedUpdate () {

		if (screenTransition == false) {

			if (playerHasFlight == true) {
				if (Input.GetKey (KeyCode.W)) {
					player.GetComponent<Rigidbody2D> ().gravityScale = 0.0f;
					player.GetComponent<Rigidbody2D> ().drag = 10.0f;
					playerIsFlying = true;
					playerIsAirborn = true;

					if (playerIsFlying == true) {
						player.GetComponent<Rigidbody2D> ().AddForce (player.transform.up * 250f);
					}
				}

				if (Input.GetKey (KeyCode.S) && playerIsFlying == true) {
					player.GetComponent<Rigidbody2D> ().AddForce (player.transform.up * -250f);
				}

			}

			if (playerHasFlight == false) {
				player.GetComponent<Rigidbody2D> ().drag = 1.0f;
				player.GetComponent<Rigidbody2D> ().gravityScale = 15.0f;
			}
		}

	}

	void OnCollisionEnter2D(Collision2D coll){

		Vector3 playerPos = new Vector3 (player.transform.position.x,
			player.transform.position.y,
			player.transform.position.z);

		if (coll.gameObject.tag == "ground") {

//			Vector3 shadowPos = new Vector3 (playerShadow.transform.position.x,
//				playerShadow.transform.position.y,
//				playerShadow.transform.position.z);

			jumpCounter = 0;
			playerIsFlying = false;
			playerIsAirborn = false;

			if (playerCanMoveRegular == true) {
				moveSpeed = 25f;
			}


//			if (playerHasShadow == true) {
//				shadowPos.y = playerPos.y + 0.5f;
//			}

			player.GetComponent<Rigidbody2D> ().drag = 1.0f;
			player.GetComponent<Rigidbody2D> ().gravityScale = 15.0f;

//			playerShadow.transform.position = shadowPos;

			player.transform.position = playerPos;

			playerInvulnerable = false;
		}

		if (coll.gameObject.tag == "enemy") {

			if (playerInvulnerable == false) {

				if (facingLeft == true) {
					rb.velocity = new Vector3 (30, 30, 0);
				}

				if (facingRight == true) {
					rb.velocity = new Vector3 (-30, 30, 0);
				}

				playerIsAirborn = true;

				moveSpeed = 0;

				health -= 1;
			}

			playerInvulnerable = true;
		}

		if (coll.gameObject.tag == "Death") {
			Application.LoadLevel ("Dead");
		}

		if (coll.gameObject.tag == "Goal" &&
		    playerHasFlight == true && playerCanDoubleJump == true && playerCanMoveRegular == true) {

			playerPos.x = -39.5f;
			playerPos.y = -39.06f;

			player.transform.position = playerPos;

			playerHasFlight = false;

			startingPlatform1.SetActive (true);
			startingPlatform2.SetActive (true);

		} else if (coll.gameObject.tag == "Goal" &&
		           playerHasFlight == false && playerCanDoubleJump == true && playerCanMoveRegular == true) {

			playerPos.x = -39.5f;
			playerPos.y = -39.06f;

			player.transform.position = playerPos;

			playerCanDoubleJump = false;

		} else if (coll.gameObject.tag == "Goal" &&
		          playerHasFlight == false && playerCanDoubleJump == false && playerCanMoveRegular == true) {

			playerPos.x = -39.5f;
			playerPos.y = -39.06f;

			player.transform.position = playerPos;

			slowZone1.SetActive (true);
			slowZone2.SetActive (true);
			slowZone3.SetActive (true);
			slowZone4.SetActive (true);

			playerCanMoveRegular = false;

			moveSpeed = 10;
		}

	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "slowZone1") {
			moveSpeed = 5;

			gameTimer.SetActive (true);

			timerCountdown = true;
		}

		if(coll.gameObject.tag == "slowZone2") {
			moveSpeed = 2.5f;
		}

		if(coll.gameObject.tag == "slowZone3") {
			moveSpeed = 1f;
		}

		if(coll.gameObject.tag == "slowZone4") {
			moveSpeed = 0f;
		}
	}
}
