using UnityEngine;
using System.Collections;
using System.IO;

public class CharacterScript : Interactible {
	public string characterName;
	public int id;
	public string defaultDialogue;
	protected bool handled;

	// Use this for initialization
	void Start () {
		//Load in dialogue based on name/id.
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Typically initates dialogue with NPC.
	public override void Interact() {
		Debug.Log ("Attempting interact with " + characterName);
		// Initiate dialogue, usually.
		DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
		dialogueScript.characterName = characterName;
		dialogueScript.text = defaultDialogue;

		//--------Examples of setting unlocks---------
		//dialogueScript.setAreaUnlock (ControllerScript.Area.MUSEUM);
		//dialogueScript.setStageUnlock (ControllerScript.Stage.MUSEUM);
		//dialogueScript.setItemUnlock (ControllerScript.Item.GAMEBRO);
		//dialogueScript.setDayUnlock(controllerScript.getNextDayUnlock());
		
		// May check GameController for quest items/progress.
		// May modify GameController for quest items/progress.
	}

}
