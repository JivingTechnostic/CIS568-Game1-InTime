using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * A convenience script for hint boxes.
 */

public class HintBoxScript : MonoBehaviour {
	public Text text;
	public Image image;
	public Text closePrompt;
	float minTime;
	float timePassed;
	ControllerScript controllerScript;

	void Start() {
		controllerScript = GameObject.Find ("GameController").GetComponent<ControllerScript> ();
		timePassed = 0;
		minTime = 0.5f;
		controllerScript.disablePlayerControl ();
	}

	public void setText(string s) {
		text.text = s;
	}

	public void setImage(string filename) {
		if (filename != null) {
			image.sprite = Resources.Load <Sprite> ("Assets/Sprites/" + filename);
		}
	}

	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed > minTime) {
			closePrompt.text = "press space to close";
		}
		if (Input.GetKeyDown("space") && timePassed > minTime) {
			controllerScript.enablePlayerControl();
			Destroy (gameObject);
		}
	}
}
