using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfirmationBoxScript : MonoBehaviour {
	public Button confirm;
	public Button deny;
	public Text textField;

	public void setText(string s) {
		textField.text = s;
	}

	public Button getConfirm() {
		return confirm;
	}
	public Button getDeny() {
		return deny;
	}
}
