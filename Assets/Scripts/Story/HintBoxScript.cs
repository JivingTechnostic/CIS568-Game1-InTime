using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * A convenience script for hint boxes.
 */

public class HintBoxScript : MonoBehaviour {
	public Text text;
	public Image image;

	public void setText(string s) {
		text.text = s;
	}

	public void setImage(string filename) {
		if (filename != null) {
			image.sprite = Resources.Load <Sprite> ("Assets/Sprites/" + filename);
		}
	}

	void Update () {
		if (Input.anyKeyDown) {
			Destroy (gameObject);
		}
	}
}
