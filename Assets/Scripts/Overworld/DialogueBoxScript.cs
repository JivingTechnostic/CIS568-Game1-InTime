using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueBoxScript : MonoBehaviour {
	public GameObject unlockAlertPrefab;

	public enum DialogueType {DEFAULT, STAGE, AREA, ITEM, DAY};
	DialogueType type;
	public string characterName;
	public string text;	// note that this could be an array, for multiple pages of text.
	ControllerScript.Area areaUnlock;
	ControllerScript.Stage stageUnlock;
	ControllerScript.Item itemUnlock;
	int dayUnlock;

	// Use this for initialization.  For time-safe operations, should begin with this disabled, set vars, then reenable.
	void Start () {
		gameObject.transform.Find ("Name").GetComponent<Text>().text = characterName;
		gameObject.transform.Find ("Text").GetComponent<Text>().text = text;
		gameObject.transform.Find ("Background").GetComponent<Image> ();
		gameObject.transform.Find ("Image").GetComponent<Image> ();
		Debug.Log (characterName + " : " + text);
	}
	
	// Update is called once per frame
	void Update () {
		// this needs to be fixed somehow.  Currently, using the same key will both create and destroy the object in the same frame.
		if (Input.anyKeyDown) {
			if (type != DialogueType.DEFAULT) {
				UnlockAlertScript alertScript = (Instantiate (unlockAlertPrefab) as GameObject).GetComponent<UnlockAlertScript>();

				switch(type) {
				case DialogueType.AREA:
					Debug.Log (areaUnlock);
					alertScript.setAreaUnlock(areaUnlock);
					break;
				case DialogueType.STAGE:
					alertScript.setStageUnlock(stageUnlock);
					break;
				case DialogueType.ITEM:
					alertScript.setItemUnlock(itemUnlock);
					break;
				case DialogueType.DAY:
					alertScript.setDayUnlock(dayUnlock);
					break;
				}
			} else {
				//allow the character to move again.
			}
			Destroy (gameObject);
		}
	}

	public void setAreaUnlock(ControllerScript.Area area) {
		areaUnlock = area;
		type = DialogueType.AREA;
	}
	public void setStageUnlock(ControllerScript.Stage stage) {
		stageUnlock = stage;
		type = DialogueType.STAGE;
	}
	public void setItemUnlock(ControllerScript.Item item) {
		itemUnlock = item;
		type = DialogueType.ITEM;
	}
	public void setDayUnlock(int days) {
		type = DialogueType.DAY;
		dayUnlock = days;
	}
}
