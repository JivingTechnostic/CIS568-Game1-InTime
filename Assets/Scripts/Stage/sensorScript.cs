using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sensorScript : MonoBehaviour {
	public GameObject door;
	private List<GameObject> inField;
	
	// Use this for initialization
	void Start () {
		inField = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	// on trigger enter add collider to array
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box") {
			inField.Add (other.gameObject);
			door.GetComponent<doorScript>().setActive(true);
		}
	}
	
	
	// on trigger exit take it away
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box") {
			inField.Remove (other.gameObject);
			if (inField.Count < 1)
				door.GetComponent<doorScript>().setActive(false);
		}
	}
	
	/*
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box") {
			door.GetComponent<doorScript>().setActive(true);
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box") {
			door.GetComponent<doorScript>().setActive(false);
		}
	}*/
}
