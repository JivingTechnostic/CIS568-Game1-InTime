using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerScript : MonoBehaviour {
	public enum Stage{MUSEUM, SUBWAY, PLANT, COMPANY, LIMBO, NONE};
	public enum Location{LAB, UNIVERSITY, MUSEUM, SUBWAY, PLANT};

	private List<Stage> availableStages;
	private List<Location> availableLocations;
	private Stage[,] playLog;

	public static ControllerScript controller;

	void Awake () {
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;
		} else if (controller != this) {
			Destroy (gameObject);
		}
		playLog = new Stage[5, 5];
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				playLog[i, j] = Stage.NONE;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isActiveDialogue() {
		return false;
	}
}
