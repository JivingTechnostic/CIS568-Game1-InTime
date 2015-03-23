using UnityEngine;
using System.Collections;

public class UniversityNPCManager : MonoBehaviour {
	public GameObject professor;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("GameController").GetComponent<ControllerScript> ().hasCutsceneBeenPlayed ("university_professor_scene")) {
			professor.SetActive (false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
