using UnityEngine;
using System.Collections;

public class AreaControllerScript : MonoBehaviour {
	public ControllerScript.Area areaCode;
	// used for unlocking stages immediately.
	public ControllerScript.Stage stageCode;
	public GameObject stageEntrance;
	GameObject gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("GameController");
		// May do some processing depending on the day.
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// This should diable and enable elements as appropriate.
	// Presumably also involves some sort of animation (fade to black and back).
	public void UpdateArea() {
		if (gameController.GetComponent<ControllerScript>().isStageUnlocked (stageCode)) {
			stageEntrance.SetActive(true);
		}
	}
}
