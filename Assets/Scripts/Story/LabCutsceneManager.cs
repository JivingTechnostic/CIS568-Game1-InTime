using UnityEngine;
using System.Collections;

public class LabCutsceneManager : CutsceneManager {
	
	// Use this for initialization
	void Start () {
		base.Start ();
		ControllerScript controllerScript = gameController.GetComponent<ControllerScript> ();
		if (controllerScript.loop == -2) {

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
