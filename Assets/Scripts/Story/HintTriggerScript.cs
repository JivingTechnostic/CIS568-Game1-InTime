using UnityEngine;
using System.Collections;

public class HintTriggerScript : MonoBehaviour {
	public string hintMessage;
	public string imageFile;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.StartsWith ("Player")) {
			HintBoxScript hintbox = GameObject.Find ("GameController").GetComponent<ControllerScript>().createHintBox();
			hintbox.setText(hintMessage);
			hintbox.setImage(imageFile);
			Destroy (gameObject);
		}
	}
}
