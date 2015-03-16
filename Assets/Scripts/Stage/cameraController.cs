using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	public GameObject player;
	public float moveTime;

	private GameObject targetPlayer;
	private Vector3 movespeed;
	private bool firstTimeSeeking;
	private Vector3 position; 

	// Use this for initialization
	void Start () {
		firstTimeSeeking = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (player && player.GetComponent<PlayerController>().isSelected()){
			trackTarget ();
		}
		else{
			player = seekTarget();
		}
		
	}

	// tracks the current target
	private void trackTarget(){
		position = player.transform.position;
		
		camera.transform.position = new Vector3(
			position.x,  
			position.y + 2,
			camera.transform.position.z
			);
	}

	// seeks a new target
	public GameObject seekTarget(){
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject p in players){
			if (p.GetComponent<PlayerController>().isSelected()){
				return p;
			}
		}
		return null;
	}
}
