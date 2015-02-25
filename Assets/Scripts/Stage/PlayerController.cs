using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	private bool selected;
	public int ID;

	// basic movement
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private bool isGrounded;

	// facing
	private int currentDirection; // 1 for right, 0 for left
	private int intendedDirection;

	// time bubble variables
	private bool inTimeBubble;
	private bool invisible;
	private Vector3 invisiblePosition;
	public Material normalState;
	public Material invisibleState;

	// stairs variables
	private GameObject inStairs;

	// key item variables
	private GameObject inKeyItem;

	// box variable
	private GameObject holdingBox;

	// panel variables
	private GameObject inPanel;

	// time wrap variables
	private bool isTimeWarping;
	private timeFieldScript fieldScript;

	void Start(){
		isGrounded = false;
		invisible = false;
		isTimeWarping = false;
		// initializes interactable variables
		inTimeBubble = false;
		inStairs = null;
		inKeyItem = null;
		inPanel = null;
		holdingBox = null;

		invisiblePosition = transform.position;

		currentDirection = 1;
		intendedDirection = 1;

		fieldScript = GetComponentInChildren<timeFieldScript> ();
	}

	void Update() {
		// check for groundedness
		//if (!isGrounded){
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -transform.up, out hit, ((BoxCollider)collider).size.y / 2f + .5f)
		    && (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Box")){
			isGrounded = true;
		}
		else{
			isGrounded = false;
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

			// handles the issue of facing
			if (currentDirection != intendedDirection){
				transform.Rotate(0,180,0);
				currentDirection = intendedDirection;
			}

			// handles interaction with w. make sure that you're grounded
			if (Input.GetKeyDown(KeyCode.W)){
				interact();
			}
			// if not currently in a bubble and not holding a box, check for time warping
			if (!invisible && holdingBox == null){
				if (Input.GetKeyDown(KeyCode.LeftShift))
					isTimeWarping = !isTimeWarping;
			}
			else if (invisible){ // if player is invisible
				// lock their positions
				transform.position = invisiblePosition;

			}
		
		} // end if selected
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
		}

		if (holdingBox != null){
			// box moves with you regardless of selected
			holdingBox.transform.parent = transform;
			holdingBox.transform.position = new Vector3(transform.position.x, 
			                                            transform.position.y + 1.5f, 
			                                            transform.position.z);
			holdingBox.rigidbody.velocity = Vector3.zero;
			holdingBox.rigidbody.mass = 1;
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
				if (Input.GetKeyDown(KeyCode.D)) // facing left
					intendedDirection = 1;
				else if (Input.GetKeyDown(KeyCode.A))
					intendedDirection = -1;

				// jumping
				if (isGrounded && Input.GetKeyDown(KeyCode.Space)){
					isGrounded = false;
					rigidbody.velocity = new Vector3 (rigidbody.velocity.x, jumpSpeed, 0);
				}
			}

		} // end if selected
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.tag == "Bubble")
			inTimeBubble = true;
		else if (c.gameObject.tag == "Stairs")
			inStairs = c.gameObject;
		else if (c.gameObject.tag == "KeyItem")
			inKeyItem = c.gameObject;
		else if (c.gameObject.tag == "Panel")
			inPanel = c.gameObject;

	}

	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "Bubble")
			inTimeBubble = false;
		else if (c.gameObject.tag == "Stairs")
			inStairs = null;
		else if (c.gameObject.tag == "KeyItem")
			inKeyItem = null;
		else if (c.gameObject.tag == "Panel")
			inPanel = null;
	}



	// handles interaction with things when pressing w
	private void interact(){
		if (holdingBox == null && GetComponentInChildren<pickUpBoxScript>().hasBox()){
			holdingBox = GetComponentInChildren<pickUpBoxScript>().getBox();
		}
		else if (inTimeBubble){
			if (invisible){
				invisible = false;
				renderer.material = normalState;
				collider.enabled = true;
				transform.position = new Vector3(invisiblePosition.x, invisiblePosition.y, invisiblePosition.z - 1.5f);
				rigidbody.velocity = new Vector3(0,0,0);
			}
			else if (!invisible && isGrounded){
				invisible = true;
				renderer.material = invisibleState;
				collider.enabled = false;
				isTimeWarping = false;
				fieldScript.timeWarp(isTimeWarping);
				invisiblePosition = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z + 1.5f);
			}
		}
		// dropping box comes after intimebubble b/c u can be in time bubble while holding box
		// if holding box and there's space
		else if (holdingBox != null ){//&& Physics.Raycast(transform.position, new Vector3(0,currentDirection, 0), 
		                            //                   ((BoxCollider)holdingBox.collider).size.x)){
			holdingBox.transform.parent = null;
			holdingBox.transform.position = new Vector3(transform.position.x + currentDirection * (((BoxCollider)collider).size.x / 2 + .1f),
			                                            transform.position.y + 0.5f, 
			                                            transform.position.z);
			holdingBox.rigidbody.mass = 10000;
			holdingBox = null;
		}

		else if (holdingBox == null && inStairs != null){
			Vector3 temp = inStairs.GetComponent<stairsScript>().connectedStairs.transform.position;
			transform.position = new Vector3(temp.x, temp.y, 0);
		}
		else if (holdingBox == null && inKeyItem != null){
			// feed it to manager
			print ("obtained!");
			inKeyItem.GetComponent<keyItemScript>().Pickup();
			Destroy(inKeyItem);
			inKeyItem = null;
		}
		else if (holdingBox == null && inPanel != null){
			inPanel.GetComponent<panelScript>().turnOff();
		}

	}

	public bool isInvisible(){
		return invisible;
	}

	public bool isInTimeWarp(){
		return isTimeWarping;
	}

	public void setSelected(bool boolean){
		selected = boolean;
	}

	public bool isSelected(){
		return selected;
	}


}
