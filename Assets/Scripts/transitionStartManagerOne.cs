using UnityEngine;
using System.Collections;

public class transitionStartManagerOne : MonoBehaviour {

	float doorTimer = 0.0f;
	bool transitionIsCounting = false;

	public meleeDoorMove meleeDoorScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (transitionIsCounting == true) {
			doorTimer += Time.deltaTime;
		}

		if (doorTimer >= 1.3f) {
			transitionIsCounting = false;

			meleeDoorScript.timeToClose ();


			doorTimer = 0.0f;
		}
	
	}

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "player") {

			GameObject.Find ("Player").GetComponent<playerMove> ().screenTransition = true;

			transitionIsCounting = true;
		}

	}
}
