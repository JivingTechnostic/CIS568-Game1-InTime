using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Event : MonoBehaviour {
	private List<Instruction> instructions;
	int activeInstruction;
	ControllerScript controllerScript;

	// Use this for initialization
	void Start () {
		instructions = new List<Instruction> ();
		loadInstructions ();
		activeInstruction = 0;
		controllerScript = GameObject.Find ("GameController").GetComponent<ControllerScript> ();
	}

	private void loadInstructions() {
		List<GameObject> unsortedInstructions = new List<GameObject>();
		foreach (Transform child in transform) {
			unsortedInstructions.Add (child.gameObject);
		}
		foreach (GameObject child in unsortedInstructions.OrderBy (go=>go.name)) {
			instructions.Add (child.GetComponent<Instruction>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (activeInstruction > instructions.Count - 1) {
			controllerScript.enablePlayerControl ();
			gameObject.SetActive(false);
		} else {
			instructions [activeInstruction].Enact ();
			controllerScript.disablePlayerControl();

			if (instructions [activeInstruction].isComplete ()) {
				activeInstruction++;
			}
		}
	}
}
