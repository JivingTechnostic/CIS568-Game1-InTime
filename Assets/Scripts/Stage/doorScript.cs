using UnityEngine;
using System.Collections;

public class doorScript : slowableObject {
	private bool isActive;
	// controls the movement of the door
	public float movementTime; 
	public float movementDistance;

	private Vector3 originalPosition;
	private float movementSpeed;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		isActive = false;
		table = new Hashtable ();
	}
	
	// Update is called once per frame
	void Update () {
		movementSpeed = movementDistance / movementTime * Time.deltaTime / timeSpeed;

		if (isActive){
			if (transform.position.y < originalPosition.y + movementDistance){
				transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed, transform.position.z);
			}
		}
		else{
			if (transform.position.y > originalPosition.y){
				transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed, transform.position.z);
			}
		}
	}

	public void setActive(bool boolean){
		isActive = boolean;
	}

}
