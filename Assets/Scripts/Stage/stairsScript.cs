using UnityEngine;
using System.Collections;

public class stairsScript : interactableObject {
	public GameObject connectedStairs;
	public AudioClip stairsSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void interact(PlayerController pc){
		if (pc.isHoldingBox () == false) {
			Vector3 temp = connectedStairs.transform.position;
			pc.transform.position = new Vector3(temp.x, temp.y, 0);
			showArrow(false);
			audio.PlayOneShot(stairsSound);
		}
	}
}
