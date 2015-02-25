using UnityEngine;
using System.Collections;

public class doorScript : MonoBehaviour {
	private bool isActive;
	// controls the movement of the door
	public float movementTime; 
	public float movementDistance;

	private Vector3 originalPosition;
	private float movementSpeed;

	private float timeSpeed = 1f;
	// each time a player is slowing this object, multiplier increases by 1
	private Hashtable table;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		isActive = false;
		table = new Hashtable ();
	}
	
	// Update is called once per frame
	void Update () {
		movementSpeed = movementDistance / movementTime * Time.deltaTime / timeSpeed;

		if (isActive){
			if (transform.position.y < originalPosition.y + movementDistance){
				transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed, transform.position.z);
			}
		}
		else{
			if (transform.position.y > originalPosition.y){
				transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed, transform.position.z);
			}
		}
	}

	public void setActive(bool boolean){
		isActive = boolean;
	}

	public void slowDown(int ID, float factor){
		table [ID] = factor;
		timeSpeed = 1;
		foreach (DictionaryEntry entry in table){
			int value = System.Convert.ToInt32(entry.Value);
			if (value > 1){
				if (timeSpeed == 1)
					timeSpeed = value;
				else
					timeSpeed += value;
			}
		}
	}
}
