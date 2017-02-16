using UnityEngine;
using System.Collections;

public class bouncingEnemySpawner : MonoBehaviour {

	public GameObject player;

	public GameObject jumpingEnemy;

	float spawnTimer = 0.0f;

	Vector3 spawnerPos;

	public playerMove thePlayer;

	public bool playerInCombat = false;

	// Use this for initialization
	void Start () {

		thePlayer = FindObjectOfType<playerMove> ();

		spawnerPos = new Vector3 (transform.position.x,
			transform.position.y,
			transform.position.z);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (transform.position, player.transform.position) < 47f) {
			playerInCombat = true;

			spawnTimer += Time.deltaTime;

			if (spawnTimer > 2f) {
				float randomDistance = Random.Range (-15, 15);

				GameObject jumpingEnemyClone = (GameObject)Instantiate (jumpingEnemy);

				Vector3 clonePos = new Vector3 (jumpingEnemyClone.transform.position.x,
					                   jumpingEnemyClone.transform.position.y,
					                   jumpingEnemyClone.transform.position.z);

				clonePos.x = spawnerPos.x + randomDistance;
				clonePos.y = spawnerPos.y;

				spawnTimer = 0.0f;

				jumpingEnemyClone.transform.position = clonePos;
			}
		} else {
			playerInCombat = false;
		}

		if (Vector3.Distance (transform.position, player.transform.position) > 50f) {
			spawnTimer = 0.0f;
		}
	}
}
