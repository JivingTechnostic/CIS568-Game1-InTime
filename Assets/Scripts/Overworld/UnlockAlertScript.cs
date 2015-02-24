using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnlockAlertScript : MonoBehaviour {
	// This is currently redundant with DialogueScript, but I think it works for now.
	enum AlertType {DEFAULT, STAGE, AREA, ITEM, DAY};
	AlertType type;
	public string text;	// note that this could be an array, for multiple pages of text.
	ControllerScript.Area areaUnlock;
	ControllerScript.Stage stageUnlock;
	ControllerScript.Item itemUnlock;

	// Use this for initialization
	void Start () {
		gameObject.transform.Find ("Text").GetComponent<Text>().text = text;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			switch(type) {
			case AlertType.AREA:
				GameObject.Find ("GameController").GetComponent<ControllerScript>().unlockArea(areaUnlock);
				break;
			case AlertType.STAGE:
				GameObject.Find ("GameController").GetComponent<ControllerScript>().unlockStage(stageUnlock);
				GameObject.Find ("AreaController").GetComponent<AreaControllerScript>().UpdateArea();
				break;
			case AlertType.ITEM:
				GameObject.Find ("GameController").GetComponent<ControllerScript>().addItem(itemUnlock);
				break;
			case AlertType.DAY:
				GameObject.Find ("GameController").GetComponent<ControllerScript>().unlockNextDay();
				break;
			}
			Destroy (gameObject);
		}
	}

	public void setAreaUnlock(ControllerScript.Area area) {
		areaUnlock = area;
		type = AlertType.AREA;
		switch (area) {
		case ControllerScript.Area.MUSEUM:
			text = "The Museum";
			break;
		case ControllerScript.Area.PLANT:
			text = "The Power Plant";
			break;
		case ControllerScript.Area.GAMESTORE:
			text = "The Game Store";
			break;
		}
		text += " has been unlocked on the map!";
	}

	public void setStageUnlock(ControllerScript.Stage stage) {
		stageUnlock = stage;
		type = AlertType.STAGE;
		switch (stage) {
		case ControllerScript.Stage.MUSEUM:
			text = "The Museum";
			break;
		case ControllerScript.Stage.SUBWAY:
			text = "The Subway";
			break;
		case ControllerScript.Stage.PLANT:
			text = "The Power Plant";
			break;
		case ControllerScript.Stage.COMPANY:
			text = "The Private Company";	// still need a name for this
			break;
		}
		// Note that limbo does not get an alert since it's a special stage.  In fact, it probably shouldn't be in ControllerScript.Stage.
		text += " has been unlocked as a stage!";
	}

	public void setItemUnlock(ControllerScript.Item item) {
		itemUnlock = item;
		type = AlertType.ITEM;
		switch (item) {
		case ControllerScript.Item.GAMEBRO:
			text = "GameBro";
			break;
		}
		text += " has been added to your inventory.";
	}

	public void setDayUnlock(int days) {
		type = AlertType.DAY;
		text = "You can now travel back " + days + " days!";
	}
}
