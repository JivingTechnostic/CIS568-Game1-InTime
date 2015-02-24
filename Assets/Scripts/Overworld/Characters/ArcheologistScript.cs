using UnityEngine;
using System.Collections;

public class ArcheologistScript : CharacterScript {
	
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
		if (!controllerScript.isAreaUnlocked(ControllerScript.Area.MUSEUM)) {
			DialogueScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueScript>();
			dialogueScript.name = name;
			dialogueScript.text = "Did you know that the museum is holding an exhibit on ancient creatures?  I hear they have real dinosaur bones!";
			dialogueScript.setAreaUnlock(ControllerScript.Area.MUSEUM);
			handled = true;
		}
		
		if (!handled) {
			base.Interact();
		}
		Debug.Log ("Attempting interact with " + name);
		handled = false;
		// Initiate dialogue, usually.
	}
	
}
