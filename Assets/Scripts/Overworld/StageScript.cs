using UnityEngine;
using System.Collections;

public class StageScript : Interactible {
	public ControllerScript.Stage stage;
	public bool [] daysOpen;	//indexing works as [-day]  Must be defined in the inspector
	GameObject gameController;
	public GameObject DialogueBoxPrefab;

	// Use this for initialization
	void Start () {
		if (!GameObject.Find ("GameController").GetComponent<ControllerScript> ().isStageUnlocked (stage)) {
			gameObject.SetActive(false);
		}
		gameController = GameObject.Find ("GameController");
	}

	// 
	public override void Interact() {
		if (!daysOpen [-gameController.GetComponent<ControllerScript> ().day]) {
			// it's closed.  present closed dialogue.
			DialogueScript dialogueScript = (Instantiate (DialogueBoxPrefab) as GameObject).GetComponent<DialogueScript>();
			dialogueScript.name = "";
			dialogueScript.text = "Looks like they're closed today.";
		} else {
			switch (stage) {
			case ControllerScript.Stage.MUSEUM:
				Application.LoadLevel ("MuseumStage");
				break;
			case ControllerScript.Stage.SUBWAY:
				Application.LoadLevel ("SubwayStage");
				break;
			case ControllerScript.Stage.PLANT:
				Application.LoadLevel ("PlantStage");
				break;
			case ControllerScript.Stage.COMPANY:
				Application.LoadLevel ("CompanyStage");
				break;
			case ControllerScript.Stage.LIMBO:
				Application.LoadLevel ("LimboStage");
				break;
			}
		}
	}
}
