using UnityEngine;
using System.Collections;

public class panelScript : MonoBehaviour {
	public GameObject securityCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void turnOff(){
		if (securityCamera.tag == "SecurityCamera")
			securityCamera.GetComponent<securityCameraScript>().setActive(false);
	}
}
