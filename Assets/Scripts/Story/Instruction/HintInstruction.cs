using UnityEngine;
using System.Collections;

public class HintInstruction : Instruction {
	public GameObject HintBoxPrefab;
	private bool isActive;
	public string message;
	public string image;
	
	void Start() {
		isActive = false;
	}
	
	public override void Enact() {
		if (!isActive) {
			HintBoxScript hintScript = (Instantiate (HintBoxPrefab) as GameObject).GetComponent<HintBoxScript>();
			hintScript.setText (message);
			hintScript.setImage (image);
			isActive = true;
		}
		
		if (isActive && Input.anyKeyDown) {
			completed = true;
		}
	}
}
