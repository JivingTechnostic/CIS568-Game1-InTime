using UnityEngine;
using System.Collections;

public class visionScript : MonoBehaviour {
	public Material normalState, discoveredState;
	public float discoveryLimit = .5f; // max time player can be in vision before being detected
	private float currentTime;

	private stageManager managerScript;
	
	// Use this for initialization
	void Start () {
		managerScript = GameObject.Find ("Manager").GetComponent<stageManager> ();
		currentTime = 0f;
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player") {
			// if player is not invisible
			if (!other.gameObject.GetComponent<PlayerController>().isInvisible()){
				renderer.material = discoveredState;
				currentTime += Time.deltaTime;
				if (currentTime > discoveryLimit){
					detected();
					currentTime = 0f;
				}
			}
			else{
				renderer.material = normalState;
				currentTime = 0f;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			renderer.material = normalState;
			currentTime = 0f;
		}
	}

	// code for when player is detected
	public void detected(){
		managerScript.respawn();
	}

}
