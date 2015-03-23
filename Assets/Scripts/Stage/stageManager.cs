using UnityEngine;
using System.Collections;

public class stageManager : MonoBehaviour {
	public GameObject[] players;
	private cameraController cameraScript;
	private ControllerScript cscript;
	public float fadeTime = .5f;
	private bool fading = false;
	private bool playAlertSound = true;
	public AudioClip alertSound;

	// Use this for initialization
	void Start () {
		if (players[0] != null) {
			players[0].GetComponent<PlayerController>().setSelected(true);
		}

		cameraScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<cameraController> ();
		cscript = GameObject.Find ("GameController").GetComponent<ControllerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		// handles player selection
		if (Input.GetKeyDown(KeyCode.Alpha1) && players[0] != null){
			enablePlayer(0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && players[1] != null){
			enablePlayer(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3) && players[2] != null){
			enablePlayer(2);
		}
	}

	// helper that takes an int 0-2 and enables that player
	private void enablePlayer(int index){
		for (int i = 0; i < players.Length; i++) {
			if (players[i] != null){
				if (i == index)
					players [i].GetComponent<PlayerController> ().setSelected (true);
				else
					players [i].GetComponent<PlayerController>().setSelected(false);
			}
		}
		//mainCamera.GetComponent<cameraController>().seekTarget();
	}

	// respawn function. locks player control and fades to black while the players are moved.
	public void respawn(){
		if (playAlertSound){
			audio.PlayOneShot(alertSound);
			playAlertSound = false;
		}

		cscript.disablePlayerControl();
		if (!fading){
			StartCoroutine (fadeOut ());
			fading = true;
		}

	}

	IEnumerator fadeOut(){
		while (cameraScript.getScreenAlpha() < 1) {
			cameraScript.setScreenAlpha(cameraScript.getScreenAlpha() + 1 * Time.deltaTime / this.fadeTime);
			if (cameraScript.getScreenAlpha() > 1)
				cameraScript.setScreenAlpha(1);
			//print (cameraScript.getScreenAlpha());
			yield return 0;
		}

		for (int i = 0; i < players.Length; i++) {
			if (players[i]){
				players[i].GetComponent<PlayerController>().respawnMove();
			}
		}
		// also reset all moving platforms
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Platform")){
			o.GetComponent<movingPlatformScript>().reset();
		}

		StartCoroutine (fadeIn ());
	}

	IEnumerator fadeIn(){
		while (cameraScript.getScreenAlpha() > 0) {
			cameraScript.setScreenAlpha(cameraScript.getScreenAlpha() - 1 * Time.deltaTime / this.fadeTime);
			if (cameraScript.getScreenAlpha() < 0)
				cameraScript.setScreenAlpha(0);
			yield return 0;
		}

		fading = false;
		cscript.enablePlayerControl ();
		playAlertSound = true;
	}
}
