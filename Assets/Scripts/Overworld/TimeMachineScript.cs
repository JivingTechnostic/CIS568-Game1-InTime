using UnityEngine;
using System.Collections;

public class TimeMachineScript : Interactible {
	
	// Use this for initialization
	void Start () {
		GameController = GameObject.Find ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Interact() {
		ControllerScript controllerScript = GameController.GetComponent<ControllerScript> ();
		if (controllerScript.day == 0) {
			if (controllerScript.canNextLoop()) {
				controllerScript.toNextLoop ();
			} else {
				DialogueScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueScript>();
				dialogueScript.name = "Me";
				dialogueScript.text = "I can't travel any futher back yet... better find more materials";
			}
		} else {
			controllerScript.toNextDay ();

		}
	}
}
