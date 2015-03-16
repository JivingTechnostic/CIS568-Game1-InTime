using UnityEngine;
using System.Collections;

public class boxScript : MonoBehaviour {
	private bool pickedUp;

	// Use this for initialization
	void Start () {
		pickedUp = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!pickedUp){
			transform.position = transform.position;
			rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
		}
	}

	public void setPickedUp(bool b){
		pickedUp = b;
	}
}
