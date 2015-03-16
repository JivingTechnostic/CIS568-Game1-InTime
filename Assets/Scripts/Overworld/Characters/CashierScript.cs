using UnityEngine;
using System.Collections;

public class CashierScript : CharacterScript {

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find ("GameController");
		handled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Interact() {
		ControllerScript controllerScript = GameController.GetComponent<ControllerScript> ();
		if (controllerScript.hasItem (ControllerScript.Item.GAMEBRO)) {
			DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
			dialogueScript.characterName = characterName;
			dialogueScript.text = "Remember to take regular breaks from gaming!";
			handled = true;
		} else if (controllerScript.day < -1) {
			DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
			dialogueScript.characterName = characterName;
			dialogueScript.text = "You want a GAMEBRO?  You're in luck!  This is our last one.";
			dialogueScript.setItemUnlock(ControllerScript.Item.GAMEBRO);
			handled = true;
		} else {
			DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
			dialogueScript.characterName = characterName;
			dialogueScript.text = "The GAMEBRO has been so popular, it's been sold out since " + controllerScript.getRelativeTermForDay(-2) + ".";
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
