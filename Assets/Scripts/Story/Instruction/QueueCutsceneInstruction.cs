using UnityEngine;
using System.Collections;

public class QueueCutsceneInstruction : Instruction {
	public string nextScene;
	
	public override void Enact() {
		GameObject.Find ("GameController").GetComponent<ControllerScript> ().startCutsceneOnLoad (nextScene);
		completed = true;
	}
}
