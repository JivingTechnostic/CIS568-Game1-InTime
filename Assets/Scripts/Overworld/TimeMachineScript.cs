using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeMachineScript : Interactible {
	public GameObject confirmationPrefab;
	ControllerScript controllerScript;

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Interact() {
		controllerScript = GameController.GetComponent<ControllerScript> ();

		if (controllerScript.day == 0) {
			if (controllerScript.canNextLoop()) {
				ConfirmationBoxScript cbox = (Instantiate (confirmationPrefab) as GameObject).GetComponent<ConfirmationBoxScript> ();
				cbox.setText ("Go to the next loop?");
				Button confirm = cbox.getConfirm ();
				confirm.onClick.AddListener(goToNextLoop);
			} else {
				DialogueBoxScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueBoxScript>();

				dialogueScript.characterName = "Me";
				dialogueScript.text = "I can't travel any futher back yet... better find more materials";
			}
		} else {
			ConfirmationBoxScript cbox = (Instantiate (confirmationPrefab) as GameObject).GetComponent<ConfirmationBoxScript> ();
			cbox.setText ("Go to the next day?");
			Button confirm = cbox.getConfirm ();
			confirm.onClick.AddListener (goToNextDay);
		}
	}

	public void goToNextDay() {
		controllerScript.toNextDay ();
	}

	public void goToNextLoop() {
		controllerScript.toNextLoop ();
	}
}
