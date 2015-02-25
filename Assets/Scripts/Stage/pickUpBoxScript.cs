using UnityEngine;
using System.Collections;

public class pickUpBoxScript : MonoBehaviour {
	private GameObject box;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.tag == "Box"){
			box = c.gameObject;
		}

	}
	
	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "Box")
			box = null;
	}

	public GameObject getBox(){
		return box;
	}

	public bool hasBox(){
		return !(box == null);
	}

	public void setBox(GameObject obj){
		box = obj;
	}
}
