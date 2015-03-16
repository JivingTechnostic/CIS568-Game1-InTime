using UnityEngine;
using UnityEditor;
using System.Collections;

public class BusinessmanScript : CharacterScript {

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find ("GameController");
		handled = false;
	}
	
	public override void Interact() {
		ControllerScript controllerScript = GameController.GetComponent<ControllerScript> ();
		Debug.Log (EditorApplication.currentScene + EditorApplication.currentScene.EndsWith ("Party.unity"));
		if (EditorApplication.currentScene.EndsWith ("Party.unity")) {
			controllerScript.startCutscene ("explosion_1");
			handled = true;
		}
		if (!handled) {
			base.Interact();
		}
		Debug.Log ("Attempting interact with " + characterName);
		handled = false;
		// Initiate dialogue, usually.
	}
}
