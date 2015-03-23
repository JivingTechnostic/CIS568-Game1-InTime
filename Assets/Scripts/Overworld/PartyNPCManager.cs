using UnityEngine;
using System.Collections;

public class PartyNPCManager : MonoBehaviour {
	public GameObject businessman;
	
	// Use this for initialization
	void Start () {
		if (GameObject.Find ("GameController").GetComponent<ControllerScript> ().hasCutsceneBeenPlayed ("explosion_1")) {
			businessman.SetActive (false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
