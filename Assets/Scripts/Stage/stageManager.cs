using UnityEngine;
using System.Collections;

public class stageManager : MonoBehaviour {
	public GameObject[] players;
	//public GameObject mainCamera;


	// Use this for initialization
	void Start () {
		if (players[0] != null) {
			players[0].GetComponent<PlayerController>().setSelected(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// handles player selection
		if (Input.GetKeyDown(KeyCode.Alpha1) && players[0] != null){
			enablePlayer(0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && players[1] != null){
			enablePlayer(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3) && players[2] != null){
			enablePlayer(2);
		}
	}

	// helper that takes an int 0-2 and enables that player
	private void enablePlayer(int index){
		for (int i = 0; i < players.Length; i++) {
			//if (players[i] != null){
				if (i == index)
					players [i].GetComponent<PlayerController> ().setSelected (true);
				else
					players [i].GetComponent<PlayerController>().setSelected(false);
			//}
		}
		//mainCamera.GetComponent<cameraController>().seekTarget();
	}
}
