using UnityEngine;
using System.Collections;

public class ReceptionistScript : CharacterScript {

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find ("GameController");
		handled = false;
	}
	
	public override void Interact() {
		Debug.Log ("Attempting interact with " + characterName);
		handled = false;
		ControllerScript controllerScript = GameController.GetComponent<ControllerScript> ();
		handled = controllerScript.startCutscene ("receptionist_scene");
		
		if (!handled) {
			base.Interact();
		}
	}
}
