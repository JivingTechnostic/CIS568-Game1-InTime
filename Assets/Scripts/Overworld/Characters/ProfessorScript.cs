using UnityEngine;
using System.Collections;
using System.IO;

public class ProfessorScript : CharacterScript {

	// Use this for initialization
	void Start () {
		//Load in dialogue based on name/id.
		GameController = GameObject.Find ("GameController");
		handled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Typically initates dialogue with NPC.
	public override void Interact() {
		ControllerScript controllerScript = GameController.GetComponent<ControllerScript> ();
		if (controllerScript.loop == 0 && controllerScript.day == 0) {
			if (!controllerScript.hasCutsceneBeenPlayed("professor_bone_scene")) {
				controllerScript.startCutscene("professor_bone_scene");
				handled = true;
			} else if (controllerScript.hasItem (ControllerScript.Item.BONES) && !controllerScript.canNextLoop ()) {
				DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
				dialogueScript.characterName = characterName;
				dialogueScript.text = "Oh wow, you found it!  Give me a minute here...";
				dialogueScript.setDayUnlock(controllerScript.getNextDayUnlock());
				handled = true;
			} else {
				DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
				dialogueScript.characterName = characterName;
				dialogueScript.text = "If we had some dinosaur bones, I could fix this up real nice.  Go ask around and see if anyone can help you get some.";
				handled = true;
			}
		} else if (controllerScript.loop >= 1 && !controllerScript.isAreaUnlocked(ControllerScript.Area.PLANT)) {
			DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
			dialogueScript.characterName = characterName;
			dialogueScript.text = "What we need now is some uranium for the laser.  Try looking into the Nuclear Power Plant for the uranium.";
			if (!controllerScript.isAreaUnlocked(ControllerScript.Area.PLANT)){
				dialogueScript.setAreaUnlock (ControllerScript.Area.PLANT);
			}
			handled = true;
		} else if (controllerScript.loop == 1 && controllerScript.day == 0) {
			if (controllerScript.hasItem (ControllerScript.Item.OIL) && !controllerScript.canNextLoop()) {
				DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
				dialogueScript.characterName = characterName;
				dialogueScript.text = "Great!  Let me put this Oil into the machine.";
				dialogueScript.setDayUnlock(controllerScript.getNextDayUnlock());
				handled = true;
			} else {
				DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
				dialogueScript.characterName = characterName;
				dialogueScript.text = "You know how there were places in the museum you couldn't get to?  If you return to a stage on the same day you completed it, in a previous loop, you'll be able to work with that loop's version of yourself to get to places you normally couldn't.  Try going back to the museum today and you'll see what I mean.";
				handled = true;
			}
		}
		if (!handled) {
			base.Interact();
		}
		handled = false;
		Debug.Log ("Attempting interact with " + characterName);
		// Initiate dialogue, usually.

		//dialogueScript.setAreaUnlock (ControllerScript.Area.MUSEUM);
		//dialogueScript.setStageUnlock (ControllerScript.Stage.MUSEUM);
		//dialogueScript.setItemUnlock (ControllerScript.Item.GAMEBRO);

		// May check GameController for quest items/progress.
		// May modify GameController for quest items/progress.
	}

}
