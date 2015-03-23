using UnityEngine;
using System.Collections;

public class doorScript : slowableObject {
	public enum moveAxis{
		vertical, horizontal
	};

	private bool isActive;
	private bool playCloseSound;
	// controls the movement of the door
	public float movementTime; 
	public float movementDistance;
	public moveAxis axis;
	public AudioClip doorOpen, doorClose;

	private Vector3 originalPosition;
	private float movementSpeed;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		isActive = false;
		table = new Hashtable ();
		playCloseSound = true;
	}
	
	// Update is called once per frame
	void Update () {
		movementSpeed = movementDistance / movementTime * Time.deltaTime / timeSpeed;

		if (axis == moveAxis.vertical){
			if (isActive){
				if (transform.position.y < originalPosition.y + movementDistance){
					transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed, transform.position.z);
					if (transform.position.y >= originalPosition.y + movementDistance && playCloseSound){
						audio.PlayOneShot(doorClose);
						playCloseSound = false;
					}
				}
			}
			else{
				if (transform.position.y > originalPosition.y){
					transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed, transform.position.z);
					if (transform.position.y <= originalPosition.y && playCloseSound){
						audio.PlayOneShot(doorClose);
						playCloseSound = false;
					}
				}
			}
		}
		else{
			if (isActive){
				if (transform.position.x < originalPosition.x + movementDistance){
					transform.position = new Vector3(transform.position.x + movementSpeed, transform.position.y, transform.position.z);
					if (transform.position.x >= originalPosition.x + movementDistance && playCloseSound){
						audio.PlayOneShot(doorClose);
						playCloseSound = false;
					}
				}
			}
			else{
				if (transform.position.x > originalPosition.x){
					transform.position = new Vector3(transform.position.x - movementSpeed, transform.position.y, transform.position.z);
					if (transform.position.x <= originalPosition.x && playCloseSound){
						audio.PlayOneShot(doorClose);
						playCloseSound = false;
					}
				}
			}
		}
	}

	public void setActive(bool boolean){
		// handles when the door is opening
		if (isActive != boolean){
			audio.PlayOneShot(doorOpen);
			playCloseSound = true;
		}
		isActive = boolean;
	}

}
