using UnityEngine;
using System.Collections;

public class targetControl : MonoBehaviour {

	public GameObject target;

	public GameObject player;

	public GameObject bullet;

	public GameObject meleeColliderRight;
	public GameObject meleeColliderLeft;

	public Vector2 pos;

	float ROF = 0f;

	float shotDelay = 0f;

	public bool AREquipped;
	public bool SGEquipped;
	public bool HCEquipped;
	public bool RLEquipped;
	public bool MLEquipped;

	public bool playerHasMeleeAttack;

	public bool playerFacingLeft;
	public bool playerFacingRight;

	public playerMove thePlayer;

	// Use this for initialization
	void Start () {

		thePlayer = FindObjectOfType<playerMove> ();

		AREquipped = true;
		SGEquipped = false;
		HCEquipped = false;
		RLEquipped = false;
		MLEquipped = false;

		playerHasMeleeAttack = true;

		playerFacingLeft = false;
		playerFacingRight = true;

		meleeColliderRight.SetActive(false);
		meleeColliderLeft.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 playerPos = new Vector3 (player.transform.position.x,
			player.transform.position.y,
			player.transform.position.z);

		Vector3 meleeColliderRightPos = new Vector3 (meleeColliderRight.transform.position.x,
			meleeColliderRight.transform.position.y,
			meleeColliderRight.transform.position.z);

		Vector3 meleeColliderLeftPos = new Vector3 (meleeColliderLeft.transform.position.x,
			meleeColliderLeft.transform.position.y,
			meleeColliderLeft.transform.position.z);

		pos = Input.mousePosition;

		pos = Camera.main.ScreenToWorldPoint(pos);

		transform.position = pos;

		if (target.transform.position.x > player.transform.position.x) {
			playerFacingRight = true;
			playerFacingLeft = false;
		}

		if (target.transform.position.x < player.transform.position.x) {
			playerFacingRight = false;
			playerFacingLeft = true;
		}

//Shooting

		if (Input.GetMouseButton(0) && (Time.time > ROF) && AREquipped == true) {

			shotDelay = 0.05f;

			ROF = Time.time + shotDelay;

			GameObject bulletClone = (GameObject)Instantiate(bullet);

			Physics2D.IgnoreCollision(bulletClone.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());

			bulletClone.transform.position = player.transform.position;
		}

//Melee Behavior

		meleeColliderRightPos.x = playerPos.x + 4;
		meleeColliderRightPos.y = playerPos.y;

		meleeColliderLeftPos.x = playerPos.x - 4;
		meleeColliderLeftPos.y = playerPos.y;

		meleeColliderRight.transform.position = meleeColliderRightPos;

		meleeColliderLeft.transform.position = meleeColliderLeftPos;

		if (Input.GetMouseButton (1) && playerHasMeleeAttack == true && playerFacingRight == true) {
			StartCoroutine(meleeRight());
		}

		if (Input.GetMouseButton (1) && playerHasMeleeAttack == true && playerFacingLeft == true) {
			StartCoroutine(meleeLeft());
		}

		Physics2D.IgnoreCollision(meleeColliderRight.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(meleeColliderLeft.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());

		player.transform.position = playerPos;

	}

	IEnumerator meleeRight(){
		float meleeCounter = 0.0f;

		while (meleeCounter < 0.15f) {
			meleeCounter += Time.deltaTime;
			meleeColliderRight.SetActive (true);

			yield return null;
		}

		meleeColliderRight.SetActive (false);
	}

	IEnumerator meleeLeft(){
		float meleeCounter = 0.0f;

		while (meleeCounter < 0.15f) {
			meleeCounter += Time.deltaTime;
			meleeColliderLeft.SetActive (true);

			yield return null;
		}

		meleeColliderLeft.SetActive (false);
	}
}
