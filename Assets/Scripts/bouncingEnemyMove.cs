using UnityEngine;
using System.Collections;

public class bouncingEnemyMove : MonoBehaviour {

	public GameObject player;

	Rigidbody2D rb;

	bool touchingGround;

	float jumpCounter = 0f;

	public playerMove thePlayer;

	// Use this for initialization
	void Start () {

		thePlayer = FindObjectOfType<playerMove> ();

		rb = GetComponent<Rigidbody2D>();

		touchingGround = false;

		player =  GameObject.Find("Player");

	}

	// Update is called once per frame
	void Update () {

		Vector3 enemyPos = new Vector3 (transform.position.x,
			transform.position.y,
			transform.position.z);

		if (Vector3.Distance (this.transform.position, player.transform.position) < 50f) {

			if (player.transform.position.x < enemyPos.x && touchingGround == true) {

				jumpCounter += Time.deltaTime;

				if (jumpCounter > 0.25f) {

					rb.velocity = new Vector3 (-10, 90, 0);

					touchingGround = false;

					jumpCounter = 0.0f;
				}
			}

			if (player.transform.position.x > enemyPos.x && touchingGround == true) {

				jumpCounter += Time.deltaTime;

				if (jumpCounter > 0.25f) {

					rb.velocity = new Vector3 (10, 90, 0);

					touchingGround = false;

					jumpCounter = 0.0f;
				}
			}
		}

		transform.position = enemyPos;

	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "ground") {
			touchingGround = true;
		}

		if (coll.gameObject.tag == "bullet") {
			Destroy (this.gameObject);
		}

		if (coll.gameObject.tag == "melee") {
			Destroy (this.gameObject);
		}

		if (coll.gameObject.tag == "player") {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "transitionEnd") {
			Destroy (this.gameObject);
		}
	}
}

