using UnityEngine;
using System.Collections;

public class DialogueInstruction : Instruction {
	public GameObject DialogueBoxPrefab;
	private bool isActive;
	public string characterName;
	public string message;
	DialogueBoxScript dialogueScript;

	void Start() {
		isActive = false;
	}

	public override void Enact() {
		if (!isActive) {
			dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();
			dialogueScript.characterName = characterName;
			dialogueScript.text = message;
			isActive = true;
		}

		if (isActive && dialogueScript == null) {
			completed = true;
		}
	}
}
