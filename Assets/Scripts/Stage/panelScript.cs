using UnityEngine;
using System.Collections;

public class panelScript : interactableObject {
	public GameObject wiredObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void interact(PlayerController pc){
		if (pc.isHoldingBox () == false) {
			if (wiredObject.tag == "SecurityCamera")
				wiredObject.GetComponent<securityCameraScript>().setActive(false);
			else if (wiredObject.tag == "Platform")
				wiredObject.GetComponent<movingPlatformScript>().startMove();
		}
	}

}
