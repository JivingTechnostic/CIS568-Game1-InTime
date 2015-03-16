using UnityEngine;
using System.Collections;

public class LocationScript : MonoBehaviour {
	public ControllerScript.Area area;

	// Use this for initialization
	void Start () {
		if (!GameObject.Find ("GameController").GetComponent<ControllerScript>().isAreaUnlocked (area)) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadArea () {
		switch (area) {
			case ControllerScript.Area.LAB:
				Application.LoadLevel ("Lab");
				break;
			case ControllerScript.Area.MUSEUM:
				Application.LoadLevel ("Museum");
				break;
			case ControllerScript.Area.PLANT:
				Application.LoadLevel ("Plant");
				break;
			case ControllerScript.Area.SUBWAY:
				Application.LoadLevel ("Subway");
				break;
			case ControllerScript.Area.UNIVERSITY:
				Application.LoadLevel ("University");
				break;
			case ControllerScript.Area.PARK:
				Application.LoadLevel ("Park");
				break;
			case ControllerScript.Area.GAMESTORE:
				Application.LoadLevel ("Store");
			break;
			case ControllerScript.Area.PARTY:
				Application.LoadLevel ("Party");
				break;
			case ControllerScript.Area.COMPANY:
				Application.LoadLevel ("Company");
				break;
		}
	}
}
