using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class timeFieldScript : MonoBehaviour {
	public Material activeState;
	public Material deactiveState;

	public float timeFactor = 3f; // number of times time will be slowed down by. e.g. 3 means times slows by 3
	
	// array of objects current in the gravityField
	private List<GameObject> inField;
	private bool isTimeWarping;

	// gets ID of its player
	private int ID;
	
	// Use this for initialization
	void Start () {
		inField = new List<GameObject> ();
		isTimeWarping = false;
		ID = ((PlayerController)gameObject.GetComponentInParent<PlayerController>()).ID;
	}

	
	// Update is called once per frame
	void Update () {
	}

	// on trigger enter add collider to array
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy" || 
		    other.gameObject.tag == "Door" ||
		    other.gameObject.tag == "SecurityCamera" ||
		    other.gameObject.tag == "Platform") {
			if (isTimeWarping){
				other.GetComponent<slowableObject>().slowDown(ID, timeFactor);
			}
			inField.Add (other.gameObject);
		}
	}
	
	
	// on trigger exit take it away
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Enemy" || 
		    other.gameObject.tag == "Door" || 
		    other.gameObject.tag == "SecurityCamera" ||
		    other.gameObject.tag == "Platform") {
			other.GetComponent<slowableObject>().slowDown(ID, 1);
			inField.Remove (other.gameObject);
		}
	}

	public void timeWarp(bool timeWarping){
		if (timeWarping) {
			renderer.material = activeState;
			isTimeWarping = true;
			slowDownInField(timeFactor);
		}
		else{
			renderer.material = deactiveState;
			isTimeWarping = false;
			slowDownInField(1);
		}
	}
	
	// checks which interatables are in gravity field's collider, switch their gravity
	public void slowDownInField(float factor){
		// flip gravity one by one
		for (int i = 0; i < inField.Count; i++) {
			if (inField[i]){
				inField[i].GetComponent<slowableObject>().slowDown(ID, factor);
			}
		}
	}

}
