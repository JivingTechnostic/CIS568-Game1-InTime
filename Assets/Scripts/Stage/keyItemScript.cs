using UnityEngine;
using System.Collections;

public class keyItemScript : interactableObject {
	public ControllerScript.Item item;
	GameObject GameController;

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find ("GameController");
	}

	override public void interact(PlayerController pc){
		if (pc.isHoldingBox() == false){
			Pickup ();
			Destroy(this.gameObject);
		}
	}

	public void Pickup() {
		ControllerScript controllerScript = GameController.GetComponent<ControllerScript> ();
		controllerScript.addItem (item);
		controllerScript.setStageToday (ControllerScript.Stage.MUSEUM);	// this can be easily resolved with a stageControllerScript and an empty stagecontroller object.
		if (Application.loadedLevelName.StartsWith ("PowerPlantStage")) {
			Application.LoadLevel("Thanks");
		} else {
			Application.LoadLevel ("Lab");
		}
	}
}
