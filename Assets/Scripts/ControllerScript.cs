using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerScript : MonoBehaviour {
	// Consider removing LIMBO as an enum value.
	public enum Stage{MUSEUM, SUBWAY, PLANT, COMPANY, LIMBO, NONE};
	public enum Area{LAB, UNIVERSITY, MUSEUM, SUBWAY, PLANT, PARK};
	public enum Item{GAMEBRO};
	
	private HashSet<Area> availableAreas;
	private HashSet<Stage> availableStages;
	private HashSet<Item> inventory;
	private Stage[,] playLog;

	public int loop;
	public int day;

	public static ControllerScript controller;

	void Awake () {
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;
		} else if (controller != this) {
			Destroy (gameObject);
		}

		// Initial set of unlocked locations
		availableAreas = new HashSet<Area> ();
		availableAreas.Add (Area.LAB);

		availableStages = new HashSet<Stage> ();

		inventory = new HashSet<Item> ();

		playLog = new Stage[5, 5];
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				playLog[i, j] = Stage.NONE;
			}
		}

		day = 0;
		loop = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isActiveDialogue() {
		return false;
	}

	public bool isStageUnlocked(Stage stage) {
		foreach (Stage s in availableStages) {
			if (stage == s) {
				return true;
			}
		}
		return false;
	}

	public bool isAreaUnlocked(Area area) {
		foreach (Area a in availableAreas) {
			if (area == a) {
				return true;
			}
		}
		return false;
	}

	public bool hasItem(Item item) {
		foreach (Item i in inventory) {
			if (item == i) {
				return true;
			}
		}
		return false;
	}

	public void unlockArea(Area area) {
		availableAreas.Add (area);
	}

	public void unlockStage(Stage stage) {
		availableStages.Add (stage);
	}

	public void addItem(Item item) {
		inventory.Add (item);
	}
}
