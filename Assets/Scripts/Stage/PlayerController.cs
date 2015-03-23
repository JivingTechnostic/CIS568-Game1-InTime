using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	private pickUpBoxScript pickupboxscript;
	private ControllerScript cscript;
	private bool selected;
	public int ID;

	//respawn
	private GameObject respawner;

	// basic movement
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private bool grounded;

	// facing
	private int currentDirection; // 1 for right, 0 for left
	private int intendedDirection;

	// time bubble variables
	private bool invisible;
	private Vector3 invisiblePosition;
	public Material normalState;
	public Material invisibleState;

	// box variable
	private GameObject holdingBox;
	// interactable object variable
	private GameObject interactableObject;

	// time wrap variables
	public float maxWarpTime = 5f;
	public float recoverWarpTime = 4f;
	private float currentWarpTime;
	public Slider timeSlider;
	private bool isTimeWarping;
	private timeFieldScript fieldScript;

	//sounds
	public AudioClip landingSound, warpActivateSound, warpDeactivateSound;


	void Start(){
		grounded = false;
		invisible = false;
		isTimeWarping = false;
		maxWarpTime -= 1; // to correct the math
		recoverWarpTime -= 1;
		currentWarpTime = maxWarpTime;
		// initializes interactable variables
		holdingBox = null;
		interactableObject = null;

		invisiblePosition = transform.position;

		currentDirection = 1;
		intendedDirection = 1;

		fieldScript = GetComponentInChildren<timeFieldScript> ();
		pickupboxscript = GetComponentInChildren<pickUpBoxScript> ();
		cscript = GameObject.Find ("GameController").GetComponent<ControllerScript> ();
	}

	void Update() {
		// only call the following if the player is selected
		if (selected){
			// make sure the player's z is on the right plane
			if (transform.position.z != 0){
				transform.position = new Vector3(transform.position.x, transform.position.y, 0);
			}
			// make sure the player's mass is 10
			if (rigidbody.mass > 10)
				rigidbody.mass = 10;

			// handles interaction with w. make sure that you're grounded
			if (Input.GetKeyDown(KeyCode.W)  && cscript.isPlayerControl()){
				interact();
			}
			// time warping
			if (Input.GetKeyDown(KeyCode.LeftShift) && cscript.isPlayerControl()){
				isTimeWarping = !isTimeWarping;
				if (isTimeWarping)
					audio.PlayOneShot(warpActivateSound);
				else
					audio.PlayOneShot(warpDeactivateSound);
			}

			// movement and jumping
			if (!invisible){
				// check for groundedness
				RaycastHit hit;
				Vector3 leftPos = new Vector3 (transform.position.x - ((BoxCollider)collider).size.x / 2,
				                               transform.position.y, transform.position.z);
				Vector3 rightPos = new Vector3 (transform.position.x + ((BoxCollider)collider).size.x / 2,
				                                transform.position.y, transform.position.z);
				if (Physics.Raycast(leftPos, -transform.up, out hit, ((BoxCollider)collider).size.y / 2f + .6f)
				    && (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Box" || hit.collider.gameObject.tag == "Platform")){
					if (!grounded)
						audio.PlayOneShot(landingSound);
					grounded = true;
				}
				else if (Physics.Raycast(rightPos, -transform.up, out hit, ((BoxCollider)collider).size.y / 2f + .6f)
				         && (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Box" || hit.collider.gameObject.tag == "Platform")){
					if (!grounded)
						audio.PlayOneShot(landingSound);
					grounded = true;
				}
				else{
					grounded = false;
				}  

				// handles the issue of facing
				if (Input.GetKeyDown(KeyCode.D) && cscript.isPlayerControl())
					intendedDirection = 1;
				else if (Input.GetKeyDown(KeyCode.A) && cscript.isPlayerControl())
					intendedDirection = -1;
				if (currentDirection != intendedDirection){
					transform.Rotate(0,180,0);
					currentDirection = intendedDirection;
				}
				// jumping
				if (grounded && Input.GetKeyDown(KeyCode.Space) && cscript.isPlayerControl()){
					//grounded = false;
					rigidbody.velocity = new Vector3 (rigidbody.velocity.x, jumpSpeed, 0);
				}

			}
			else if (invisible){ // if player is invisible
				// lock their positions
				transform.position = invisiblePosition;

			}
		
		} // if not selected
		else{
			// if the player isn't active but still is invisible, make sure to lock its position
			if (invisible)
				transform.position = invisiblePosition;
			else{
				// if ther player is holding something, set its mass to 1000
				if (holdingBox != null)
					rigidbody.mass = 1000;
				// player isn't active or invisible set its z to the back plane
				else if (transform.position.z != 1.5f){
					transform.position = new Vector3(transform.position.x, transform.position.y, 1.5f);
				}
			}
			// locking player velocity so it doesnt slide horizontally
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}

		// box moves with you regardless of selected
		if (holdingBox != null){
			holdingBox.transform.parent = transform;
			holdingBox.transform.position = new Vector3(transform.position.x, 
			                                            transform.position.y + 1.9f,
			                                            //transform.position.y + (transform.localScale.y + (holdingBox.transform.localScale.y * transform.localScale.y)) / 2 + .1f, 
			                                            transform.position.z);
			holdingBox.rigidbody.velocity = Vector3.zero;
			holdingBox.rigidbody.mass = .01f;
		}
		// do time warping regardless of selected
		if (isTimeWarping) {
			currentWarpTime -= Time.deltaTime;
			if (currentWarpTime < 0){
				currentWarpTime = 0;
				isTimeWarping = false;
				audio.PlayOneShot(warpDeactivateSound);
			}
		}
		else{
			currentWarpTime += Time.deltaTime * maxWarpTime / recoverWarpTime;
			if (currentWarpTime > maxWarpTime){
				currentWarpTime = maxWarpTime;
			}
		}
		timeSlider.value = currentWarpTime / maxWarpTime;
		fieldScript.timeWarp(isTimeWarping);
	}

	// updates for physics stuff
	void FixedUpdate () {
		// all the following only takes place if the player is selected
		if (selected){

			// all movement can only happen is you're not invisible
			if (!invisible){
				//horizontal movement.
				float move = Input.GetAxis ("Horizontal");
				if (cscript.isPlayerControl()){
					rigidbody.velocity = new Vector3 (move * speed, rigidbody.velocity.y, 0);
				}
				else{
					rigidbody.velocity = Vector3.zero;
				}

			}

		} // end if selected
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.tag == "Bubble" || c.gameObject.tag == "Stairs" || c.gameObject.tag == "KeyItem" || c.gameObject.tag == "Panel"){
			interactableObject = c.gameObject;
			//print(interactableObject.GetComponentInChildren<Transform>().gameObject.name);
			interactableObject.GetComponent<interactableObject>().showArrow(true);
		}
		else if(c.gameObject.tag == "Respawn")
			respawner = c.gameObject;
	}

	void OnTriggerExit(Collider c){
		if (interactableObject != null && c.gameObject == interactableObject)
			interactableObject.GetComponent<interactableObject>().showArrow(false);
			//interactableObject.GetComponentInChildren<MeshRenderer>().enabled = true;
			interactableObject = null;
	}



	// handles interaction with things when pressing w
	private void interact(){
		// handles interactable objects
		if (interactableObject != null)
			interactableObject.GetComponent<interactableObject>().interact(this);

		// handles box stuff
		if (holdingBox == null && pickupboxscript.hasBox()){
			holdingBox = GetComponentInChildren<pickUpBoxScript>().getBox();
			holdingBox.GetComponent<boxScript>().setPickedUp(true);
		}
		else if (holdingBox != null ){
			holdingBox.GetComponent<boxScript>().setPickedUp(false);
			holdingBox.transform.parent = null;
			holdingBox.transform.position = new Vector3(transform.position.x + currentDirection * (((BoxCollider)collider).size.x / 2 + .6f),
			                                            transform.position.y, 
			                                            transform.position.z);
			holdingBox.rigidbody.mass = 10000;
			holdingBox = null;
		}
	}


	// sends player back to respawner location
	public void respawnMove(){
		currentWarpTime = maxWarpTime;
		transform.position = respawner.transform.position;
		isTimeWarping = false;
		if (holdingBox){
			GameObject temp = holdingBox;
			holdingBox = null;
			temp.GetComponent<boxScript>().reset();
		}
	}

	public bool isInvisible(){
		return invisible;
	}

	public void setInvisible(bool b){
		invisible = b;
	}

	public void setInvisiblePosition(Vector3 v){
		invisiblePosition = v;
	}

	public bool isGrounded(){
		return grounded;
	}

	public bool isInTimeWarp(){
		return isTimeWarping;
	}

	public void setTimeWarping(bool b){
		isTimeWarping = b;
	}

	public bool isHoldingBox(){
		return !(holdingBox == null);
	}

	public void setSelected(bool boolean){
		selected = boolean;
		timeSlider.GetComponent<sliderScript> ().setSelected (selected);
	}

	public bool isSelected(){
		return selected;
	}

}
