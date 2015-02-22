using UnityEngine;
using System.Collections;

public class characterMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Left"))	 {
			transform.Translate (new Vector3(1, 0));
		} else if (Input.GetButtonDown ("Right")) {

		}
	}
}
