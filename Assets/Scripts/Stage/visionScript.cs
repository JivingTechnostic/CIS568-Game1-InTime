using UnityEngine;
using System.Collections;

public class visionScript : MonoBehaviour {
	public Material normalState, discoveredState;
	
	// Use this for initialization
	void Start () {

	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player") {
			// if player is not invisible
			if (!other.gameObject.GetComponent<PlayerController>().isInvisible())
				renderer.material = discoveredState;
			else{
				renderer.material = normalState;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			renderer.material = normalState;
		}
	}

}
