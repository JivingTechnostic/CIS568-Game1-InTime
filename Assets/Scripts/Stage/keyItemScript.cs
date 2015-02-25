using UnityEngine;
using System.Collections;

public class keyItemScript : MonoBehaviour {
	public ControllerScript.Item item;
	GameObject GameController;

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find ("GameController");
	}

	public void Pickup() {
		ControllerScript controllerScript = GameController.GetComponent<ControllerScript> ();
		controllerScript.addItem (item);
		controllerScript.setStageToday (ControllerScript.Stage.MUSEUM);	// this can be easily resolved with a stageControllerScript and an empty stagecontroller object.
		Application.LoadLevel ("Lab");
	}
}
