using UnityEngine;
using System.Collections;

public class Interactible : MonoBehaviour {
	protected GameObject GameController;
	public GameObject DialogueBoxPrefab;

	// Use this for initialization
	void Start () {
		GameController = GameObject.Find ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// This function will be defined differently for each interactible object.
	public virtual void Interact() {

	}
}
