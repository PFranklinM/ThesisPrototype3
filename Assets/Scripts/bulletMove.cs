using UnityEngine;
using System.Collections;

public class bulletMove : MonoBehaviour {

	float moveSpeed = 75f;

	public GameObject target;
	public GameObject Bullet;
	public GameObject player;

	// Use this for initialization
	void Start () {

		target = GameObject.Find("Target");
		player = GameObject.Find("Player");
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.MoveTowards (transform.position,
			target.transform.position,
			Time.deltaTime * moveSpeed);

		if (GameObject.Find ("Target").GetComponent<targetControl> ().AREquipped) {

			if (Vector3.Distance (transform.position, player.transform.position) >= 20) {
				Destroy (Bullet);
			}

			if (Vector3.Distance (transform.position, target.transform.position) <= 0.5f) {
				Destroy (Bullet);
			}
		}
	
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "enemy") {
			Destroy (Bullet);
		}

		if (coll.gameObject.tag == "ground") {
			Destroy (Bullet);
		}

		if (coll.gameObject.tag == "ceiling") {
			Destroy (Bullet);
		}

		if (coll.gameObject.tag == "wall") {
			Destroy (Bullet);
		}

		if (coll.gameObject.tag == "door") {
			Destroy (Bullet);
		}
	}
}
