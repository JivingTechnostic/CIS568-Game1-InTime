using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	private pickUpBoxScript pickupboxscript;
	private bool selected;
	public int ID;

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
	private bool isTimeWarping;
	private timeFieldScript fieldScript;

	void Start(){
		grounded = false;
		invisible = false;
		isTimeWarping = false;
		// initializes interactable variables
		holdingBox = null;
		interactableObject = null;

		invisiblePosition = transform.position;

		currentDirection = 1;
		intendedDirection = 1;

		fieldScript = GetComponentInChildren<timeFieldScript> ();
		pickupboxscript = GetComponentInChildren<pickUpBoxScript> ();
	}

	void Update() {
		// check for groundedness
		//if (!grounded){
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -transform.up, out hit, ((BoxCollider)collider).size.y / 2f + .6f)
		    && (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Box" || hit.collider.gameObject.tag == "Platform")){
			grounded = true;
		}
		else{
			grounded = false;
		}
		
		//}  

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
			if (Input.GetKeyDown(KeyCode.W)){
				interact();
			}

			// code for movement and jump and warpfields
			if (!invisible){
				// handles the issue of facing
				if (Input.GetKeyDown(KeyCode.D))
					intendedDirection = 1;
				else if (Input.GetKeyDown(KeyCode.A))
					intendedDirection = -1;
				if (currentDirection != intendedDirection){
					transform.Rotate(0,180,0);
					currentDirection = intendedDirection;
				}
				// jumping
				if (grounded && Input.GetKeyDown(KeyCode.Space)){
					grounded = false;
					rigidbody.velocity = new Vector3 (rigidbody.velocity.x, jumpSpeed, 0);
				}
				// time warping
				if (holdingBox == null)
					if (Input.GetKeyDown(KeyCode.LeftShift))
						isTimeWarping = !isTimeWarping;


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
			// locking player velocity so it doesnt slide
			rigidbody.velocity = Vector3.zero;
		}

		// box moves with you regardless of selected
		if (holdingBox != null){
			holdingBox.transform.parent = transform;
			holdingBox.transform.position = new Vector3(transform.position.x, 
			                                            transform.position.y + 1.75f, 
			                                            transform.position.z);
			holdingBox.rigidbody.velocity = Vector3.zero;
			holdingBox.rigidbody.mass = .01f;
		}
		// do time warping regardless of selected
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
				rigidbody.velocity = new Vector3 (move * speed, rigidbody.velocity.y, 0);

			}

		} // end if selected
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.tag == "Bubble" ||
		    c.gameObject.tag == "Stairs" ||
		    c.gameObject.tag == "KeyItem" ||
		    c.gameObject.tag == "Panel")
			interactableObject = c.gameObject;

	}

	void OnTriggerExit(Collider c){
		if (interactableObject != null && c.gameObject == interactableObject)
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
			holdingBox.transform.position = new Vector3(transform.position.x + currentDirection * (((BoxCollider)collider).size.x / 2 + 1f),
			                                            transform.position.y + 1f, 
			                                            transform.position.z);
			holdingBox.rigidbody.mass = 10000;
			holdingBox = null;
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
	}

	public bool isSelected(){
		return selected;
	}


}
