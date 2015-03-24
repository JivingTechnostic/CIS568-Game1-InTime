using UnityEngine;
using System.Collections;

public class timeBubbleScript : interactableObject {
	public AudioClip bubbleSound;
	private bool occupied;
	
	// Use this for initialization
	void Start () {
		occupied = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void OnTriggerStay(Collider c){
		if (c.gameObject.tag == "Enemy"){
			occupied = true;
		}
	}
	
	
	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "Enemy"){
			occupied = false;
		}
	}
	
	
	override public void interact(PlayerController pc){
		if (!occupied && pc.isInvisible()){
			pc.setInvisible(false);
			pc.renderer.material = pc.normalState;
			pc.collider.enabled = true;
			pc.transform.position = new Vector3(pc.transform.position.x, pc.transform.position.y, pc.transform.position.z - 1.5f);
			pc.rigidbody.velocity = new Vector3(0,0,0);
		}
		else if (pc.isInvisible() == false && pc.isGrounded()){
			pc.setInvisible(true);
			pc.renderer.material = pc.invisibleState;
			pc.collider.enabled = false;
			pc.setInvisiblePosition(new Vector3(pc.transform.position.x, pc.transform.position.y + .5f, pc.transform.position.z + 1.5f));
		}
		audio.PlayOneShot(bubbleSound);
	}
}
