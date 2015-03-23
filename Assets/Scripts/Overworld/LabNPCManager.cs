using UnityEngine;
using System.Collections;

public class LabNPCManager : MonoBehaviour {
	public GameObject professor;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("GameController").GetComponent<ControllerScript> ().hasCutsceneBeenPlayed ("university_professor_scene")) {
			professor.SetActive (true);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
