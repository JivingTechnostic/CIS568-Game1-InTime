using UnityEngine;
using System.Collections;

public class boxScript : interactableObject {
	private bool pickedUp;
	private Vector3 originalPos;

	public AudioClip pickupSound, putdownSound;

	// Use this for initialization
	void Start () {
		pickedUp = false;
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!pickedUp){
			transform.position = transform.position;
			rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
		}
		else{
			showArrow(false);
		}
	}

	public void setPickedUp(bool b){
		// handles when the box is picked up
		if (pickedUp == false && b == true){
			audio.PlayOneShot(pickupSound);
		}
		// handles when the box is dropped
		if (pickedUp == true && b == false){
			audio.PlayOneShot(putdownSound);
			showArrow(true);
			transform.rotation = Quaternion.Euler(0,0,0);
		}

		pickedUp = b;

	}

	override public void interact(PlayerController pc){
		// nothing
	}

	public void reset(){
		pickedUp = false;
		transform.parent = null;
		transform.position = originalPos;
	}


}
