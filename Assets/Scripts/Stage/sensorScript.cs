using UnityEngine;
using System.Collections;

public class sensorScript : MonoBehaviour {
	public GameObject door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player") {
			door.GetComponent<doorScript>().setActive(true);
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			door.GetComponent<doorScript>().setActive(false);
		}
	}
}
