using UnityEngine;
using System.Collections;

public class GateGuardScript : CharacterScript {
	
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
			if (controllerScript.isStageUnlocked(ControllerScript.Stage.PLANT)) {
				DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
				dialogueScript.characterName = characterName;
				dialogueScript.text = "Just 10 more minutes... then I'll stop.";
				handled = true;
			} else {
				DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
				dialogueScript.characterName = characterName;
				dialogueScript.text = "Oh wow, is this GAMEBRO for me?  Thanks!  I'm so distracted now, anyone could potentially sneak into the plant!";
				dialogueScript.setStageUnlock(ControllerScript.Stage.PLANT);
				handled = true;
			}
		} else {
			DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
			dialogueScript.characterName = characterName;
			dialogueScript.text = "Sigh.  I wish I could go to the game store and buy a GAMEBRO to distract myself with, but I'm stuck on duty...";
			if (!controllerScript.isAreaUnlocked(ControllerScript.Area.GAMESTORE)) {
				dialogueScript.setAreaUnlock(ControllerScript.Area.GAMESTORE);
			}
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
