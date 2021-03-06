﻿using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour {
	float walkingSpeed;
	public ControllerScript controller;
	public Animation walkCycle;
	bool tryInteract;

	// Use this for initialization
	void Start () {
		walkingSpeed = 0.05f;
		tryInteract = false;
		controller = GameObject.Find ("GameController").GetComponent<ControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!controller.isPlayerControl()) {
			return;
		}

		if (GameObject.Find ("DialogueBoxPrefab(Clone)") || GameObject.Find ("UnlockAlertPrefab(Clone)")) {
		} else {
			if (Input.GetButtonDown ("Interact")) {
				tryInteract = true;
			}
			if (controller.isActiveDialogue()) {
			} else if (Input.GetAxisRaw("Horizontal") > 0) {
				transform.Translate (new Vector3(walkingSpeed, 0));
				tryInteract = false;
			} else if (Input.GetAxisRaw("Horizontal") < 0) {
				transform.Translate (new Vector3(-walkingSpeed, 0));
				tryInteract = false;
			}
		}
	}

	void OnTriggerStay(Collider col) {
		if (tryInteract && !controller.isActiveDialogue()) {
			Debug.Log ("Attempting Interaction");
			col.gameObject.GetComponent<Interactible>().Interact();
			tryInteract = false;
		}
	}
}
