﻿using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour {
	float walkingSpeed;
	public ControllerScript controller;
	bool tryInteract;

	// Use this for initialization
	void Start () {
		walkingSpeed = 0.05f;
		tryInteract = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Interact")) {
			tryInteract = true;
		}
		if (controller.isActiveDialogue()) {
		} else if (Input.GetAxisRaw("Horizontal") > 0) {
			transform.Translate (new Vector3(walkingSpeed, 0));
		} else if (Input.GetAxisRaw("Horizontal") < 0) {
			transform.Translate (new Vector3(-walkingSpeed, 0));
		}
	}

	void OnTriggerStay(Collider col) {
		if (tryInteract && !controller.isActiveDialogue()) {
			Debug.Log ("Attempted speech");
			tryInteract = false;
		}
	}
}