using UnityEngine;
using System.Collections;

public abstract class interactableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	abstract public void interact(PlayerController pc);
}
