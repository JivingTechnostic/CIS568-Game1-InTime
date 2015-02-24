using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerScript : MonoBehaviour {
	// Consider removing LIMBO as an enum value.
	public enum Stage{MUSEUM, SUBWAY, PLANT, COMPANY, LIMBO, NONE};
	public enum Area{LAB, UNIVERSITY, MUSEUM, SUBWAY, PLANT, PARK, GAMESTORE};
	public enum Item{GAMEBRO, BONES, URANIUM};

	private HashSet<Area> availableAreas;
	private HashSet<Stage> availableStages;
	private HashSet<Item> inventory;
	private int furthestBack;
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
		availableAreas.Add (Area.PARK);

		availableStages = new HashSet<Stage> ();
		availableStages.Add (Stage.MUSEUM);

		inventory = new HashSet<Item> ();
		inventory.Add (Item.BONES);

		playLog = new Stage[5, 5];
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				playLog[i, j] = Stage.NONE;
			}
		}

		furthestBack = 0;
		day = 0;
		loop = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		GUILayout.BeginArea (new Rect (10, 10, 100, 20));
		GUILayout.TextArea ("Loop " + loop + ", Day " + day);
		GUILayout.EndArea ();
	}

	public void toNextDay() {
		day++;
	}

	public void toNextLoop() {
		loop++;
		day = -loop;
	}

	public bool canNextLoop() {
		return -furthestBack > loop;
	}

	public bool isActiveDialogue() {
		return false;
	}

	public bool isStageUnlocked(Stage stage) {
		if (stage == Stage.NONE) {
			return false;
		}
		foreach (Stage s in availableStages) {
			if (stage == s) {
				return true;
			}
		}
		return false;
	}

	public bool isAreaUnlocked(Area area) {
		return availableAreas.Contains (area);
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

	public void unlockNextDay() {
		furthestBack--;
	}

	public int getNextDayUnlock() {
		return -(furthestBack - 1);
	}

	public string getRelativeTermForDay(int day) {
		switch (this.day - day) {
		case -1:
			return "tomorrow";
		case 0:
			return "today";
		case 1:
			return "yesterday";
		case 2:
			return "two days ago";
		case 3:
			return "three days ago";
		}
		return "today";
	}
}
