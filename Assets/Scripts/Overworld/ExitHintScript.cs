using UnityEngine;
using System.Collections;

public class ExitHintScript : MonoBehaviour {
	public GameObject hintText;

	// Use this for initialization
	void Start () {
		hintText.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.name.Equals ("Player")) {
			hintText.SetActive (true);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.name.Equals ("Player")) {
			hintText.SetActive (false);
		}
	}
}
