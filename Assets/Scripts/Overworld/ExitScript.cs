using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.name.Equals ("Player")) {
			Application.LoadLevel("Map");
			Debug.Log ("Going to overworld");
		}
	}
}
