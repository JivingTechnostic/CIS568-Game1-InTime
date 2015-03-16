using UnityEngine;
using System.Collections;

public class LoadInstruction : Instruction {
	public string scene;
	public string cutsceneOnLoad;
	
	// Use this for initialization
	void Start () {
	}
	
	public override void Enact() {
		ControllerScript controllerScript = GameObject.Find ("GameController").GetComponent <ControllerScript> ();
		completed = true;
		if (cutsceneOnLoad != null) {
			controllerScript.startCutsceneOnLoad (cutsceneOnLoad);
		}
		Application.LoadLevel (scene);
	}
}
