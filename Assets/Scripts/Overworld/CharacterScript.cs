using UnityEngine;
using System.Collections;
using System.IO;

public class CharacterScript : Interactible {
	public string name;
	public int id;
	public GameObject dialogueBoxPrefab;
	public string defaultDialogue;

	// Use this for initialization
	void Start () {
		//Load in dialogue based on name/id.
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Typically initates dialogue with NPC.
	public override void Interact() {
		Debug.Log ("Attempting interact with " + name);
		// Initiate dialogue, usually.
		DialogueScript dialogueScript = (Instantiate (dialogueBoxPrefab) as GameObject).GetComponent<DialogueScript>();
		dialogueScript.name = name;
		dialogueScript.text = defaultDialogue;
		//dialogueScript.setAreaUnlock (ControllerScript.Area.MUSEUM);
		dialogueScript.setStageUnlock (ControllerScript.Stage.MUSEUM);
		//dialogueScript.setItemUnlock (ControllerScript.Item.GAMEBRO);

		// May check GameController for quest items/progress.
		// May modify GameController for quest items/progress.
	}

}
