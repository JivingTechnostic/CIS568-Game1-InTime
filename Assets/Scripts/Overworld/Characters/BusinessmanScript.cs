using UnityEngine;
using System.Collections;

public class BusinessmanScript : CharacterScript {

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find ("GameController");
		handled = false;
	}
	
	public override void Interact() {
		Debug.Log ("Attempting interact with " + characterName);
		handled = false;
		ControllerScript controllerScript = GameController.GetComponent<ControllerScript> ();
		if (Application.loadedLevelName.Equals ("Party")) {
			controllerScript.startCutscene ("explosion_1");
			handled = true;
		}
		if (!handled) {
			base.Interact();
		}
		// Initiate dialogue, usually.
	}
}
