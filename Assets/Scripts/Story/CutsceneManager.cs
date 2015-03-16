using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutsceneManager : MonoBehaviour {
	public Dictionary <string, GameObject> cutscenes = new Dictionary<string, GameObject>();
	protected GameObject gameController;

	// Use this for initialization
	public void Start () {
		loadCutscenes ();
		gameController = GameObject.Find ("GameController");
		gameController.GetComponent<ControllerScript> ().setCutsceneManager (gameObject);
		playScene(gameController.GetComponent<ControllerScript>().getNextCutscene());
	}

	private void loadCutscenes() {
		foreach (Transform child in GameObject.Find ("Cutscenes").transform) {
			GameObject cutscene = child.gameObject;
			Debug.Log (cutscene);
			cutscenes.Add (cutscene.name, cutscene);
		}
	}

	public void playScene(string name) {
		Debug.Log ("Playing scene " + name);
		if (name != null && cutscenes.ContainsKey(name)) {
			cutscenes[name].SetActive (true);
		}
	}
}
