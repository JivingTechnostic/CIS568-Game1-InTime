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
	int msPerChar;
	int msPassed;
	int minTimeMS;
	bool fullTextDisplayed;
	Text bodyText;
	ControllerScript controllerScript;

	// Use this for initialization.  For time-safe operations, should begin with this disabled, set vars, then reenable.
	void Start () {
		gameObject.transform.Find ("Name").GetComponent<Text>().text = characterName;
		bodyText = gameObject.transform.Find ("Text").GetComponent<Text>();
		gameObject.transform.Find ("Background").GetComponent<Image> ();
		gameObject.transform.Find ("Image").GetComponent<Image> ();
		Debug.Log (characterName + " : " + text);
		msPerChar = 25;
		msPassed = 0;
		minTimeMS = 250;
		fullTextDisplayed = false;
		controllerScript = GameObject.Find ("GameController").GetComponent<ControllerScript> ();
		controllerScript.disablePlayerControl ();
	}
	
	// Update is called once per frame
	void Update () {
		msPassed += (int)(Time.deltaTime * 1000);
		if (!fullTextDisplayed) {
			int index = msPassed / msPerChar;
			if (index > text.Length) {
				fullTextDisplayed = true;
				index = text.Length;
			}
			bodyText.text = text.Substring(0, index);
		} else if (msPassed > minTimeMS) {
			gameObject.transform.Find ("NextPrompt").GetComponent<Text>().text = "press space to continue";
		}
		if (Input.GetKeyDown ("space")) {
		    if (!fullTextDisplayed) {
				bodyText.text = text;
				fullTextDisplayed = true;
			} else if (msPassed > minTimeMS) {
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
				controllerScript.enablePlayerControl();
				Destroy (gameObject);
			}
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
