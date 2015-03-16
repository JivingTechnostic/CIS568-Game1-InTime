﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Event : MonoBehaviour {
	private List<Instruction> instructions;
	int activeInstruction;

	// Use this for initialization
	void Start () {
		instructions = new List<Instruction> ();
		loadInstructions ();
		activeInstruction = 0;
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
			gameObject.SetActive(false);
		} else {
			instructions [activeInstruction].Enact ();
			
			if (instructions [activeInstruction].isComplete ()) {
				activeInstruction++;
			}
		}
	}
}