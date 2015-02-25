using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	public GameObject player;
	Vector3 position; 

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
			seekTarget();
		
		if (player)
			trackTarget ();
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
	public void seekTarget(){
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject p in players){
			if (p.GetComponent<PlayerController>().isSelected()){
				player = p;
				break;
			}
		}
	}
}
