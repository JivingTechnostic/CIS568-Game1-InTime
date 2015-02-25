using UnityEngine;
using System.Collections;

public class securityCameraScript : MonoBehaviour {
	private bool isActive;
	// controls the movement of the door
	public float rotationTime;
	// left and right are whole numbers indicating how much to rotate to the left/right. 
	public float rightRotation;
	public float leftRotation;
	public float rotationWait;
	
	private float rotationDistance;
	private float currentRotation;
	private float rotationDirection;
	private float rotationSpeed;
	
	private float timeSpeed = 1f;
	private Hashtable table;

	// Use this for initialization
	void Start () {
		rotationDirection = 1f;
		currentRotation = 0f;
		isActive = true;
		rotationDistance = rightRotation + leftRotation;

		table = new Hashtable ();

		// if both are 0, then camera is stationary
		if (leftRotation + rightRotation != 0){
			transform.Rotate (0, 0, -leftRotation);
			StartCoroutine (autoRotate ());
		}
	}
	
	// Update is called once per frame
	void Update () {		
		rotationSpeed = rotationDistance / rotationTime * Time.deltaTime / timeSpeed;

		// turn vision off if camera is off
		if (isActive == false)
			transform.GetChild(0).gameObject.SetActive(false);
	}

	IEnumerator autoRotate(){
		while (isActive) {
			while (isActive && currentRotation < rotationDistance){
				transform.Rotate(0,0,rotationSpeed * rotationDirection);
				currentRotation += rotationSpeed;
				yield return 0;
			}

			currentRotation = 0f;
			rotationDirection *= -1;
			yield return new WaitForSeconds(rotationWait);

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
