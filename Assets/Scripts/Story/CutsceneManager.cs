using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutsceneManager : MonoBehaviour {
	public Dictionary <string, GameObject> cutscenes = new Dictionary<string, GameObject>();
	protected ControllerScript controller;

	// Use this for initialization
	public void Start () {
		loadCutscenes ();
		controller = GameObject.Find ("GameController").GetComponent<ControllerScript> ();
		controller.setCutsceneManager (gameObject);
		playScene(controller.getNextCutscene());
	}

	private void loadCutscenes() {
		foreach (Transform child in GameObject.Find ("Cutscenes").transform) {
			GameObject cutscene = child.gameObject;
			Debug.Log (cutscene);
			cutscenes.Add (cutscene.name, cutscene);
		}
	}

	public bool playScene(string name) {
		if (name != null && cutscenes.ContainsKey(name) && !controller.hasCutsceneBeenPlayed(name)) {
			controller.cutscenePlayed(name);
			controller.cutscenePlayedSuccessfully();
			cutscenes[name].SetActive (true);
			Debug.Log ("Playing scene " + name);
			return true;
		}
		Debug.Log ("Failed to play scene " + name);
		return false;
	}
}
