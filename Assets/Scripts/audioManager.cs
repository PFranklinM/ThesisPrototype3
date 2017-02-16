using UnityEngine;
using System.Collections;

public class audioManager : MonoBehaviour {

	public AudioSource theAudio;

	public AudioClip track1;
	public AudioClip track2;

	float audio1Volume = 1.0f;
	float audio2Volume = 0.0f;
	bool track1Playing = true;
	bool track2Playing = false;

	public bouncingEnemySpawner combatManager;

	// Use this for initialization
	void Start () {

		theAudio.clip = track1;
		theAudio.Play();

		combatManager = FindObjectOfType<bouncingEnemySpawner> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (combatManager.playerInCombat == true) {

			fadeOutSong1 ();

			if (audio1Volume <= 0.1) {
				track1Playing = false;
				if (track2Playing == false) {
					track2Playing = true;
					theAudio.clip = track2;
					theAudio.Play ();
				}

				fadeInSong2 ();
			}
		}

		if (combatManager.playerInCombat == false) {

			fadeOutSong2 ();

			if (audio2Volume <= 0.1) {
				track2Playing = false;
				if (track1Playing == false) {
					track1Playing = true;
					theAudio.clip = track1;
					theAudio.Play ();
				}

				fadeInSong1 ();
			}
		}
	}

	void fadeInSong2() {
		if (audio2Volume < 1) {
			audio2Volume += 1f * Time.deltaTime;
			theAudio.volume = audio2Volume;
		}
	}

	void fadeOutSong1() {
		if (audio1Volume > 0.1) {
			audio1Volume -= 1f * Time.deltaTime;
			theAudio.volume = audio1Volume;
		}
	}

	void fadeInSong1() {
		if (audio1Volume < 1) {
			audio1Volume += 1f * Time.deltaTime;
			theAudio.volume = audio1Volume;
		}
	}

	void fadeOutSong2() {
		if (audio2Volume > 0.1) {
			audio2Volume -= 1f * Time.deltaTime;
			theAudio.volume = audio2Volume;
		}
	}

	//	void OnGUI(){
	//		GUI.Label(new Rect(10, 10, 200, 100), "Audio 1 : " + audio1Volume.ToString());
	//		GUI.Label(new Rect(10, 30, 200, 100), "Audio 2 : " + audio2Volume.ToString());
	//	}
}
