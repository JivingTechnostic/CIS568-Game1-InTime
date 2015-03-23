using UnityEngine;
using System.Collections;

public abstract class interactableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.tag == "Player"){
			showArrow(true);
		}
	}
	
	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "Player"){
			showArrow(false);
		}
	}

	public void showArrow(bool b){
		transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = b;
	}
	abstract public void interact(PlayerController pc);
}
