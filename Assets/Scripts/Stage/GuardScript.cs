using UnityEngine;
using System.Collections;

public class GuardScript : slowableObject {
	public float patrolTime = 4;
	public float rightDistance = 5;
	public float leftDistance = 5;
	public float patrolWait = 3f;
	
	private float patrolDistance;
	private float currentDistance;
	private float currentDirection;
	private float patrolSpeed;



	// Use this for initialization
	void Start () {
		currentDistance = 0f;
		patrolDistance = rightDistance + leftDistance;

		transform.position = new Vector3 (transform.position.x - leftDistance, transform.position.y, transform.position.z);
		StartCoroutine (patrol ());
	}
	
	// Update is called once per frame
	void Update () {
		patrolSpeed = patrolDistance / patrolTime * Time.deltaTime / timeSpeed;
	}

	IEnumerator patrol(){
		while (true) {
			while (currentDistance < patrolDistance){
				transform.Translate(patrolSpeed,0,0);
				currentDistance += patrolSpeed;
				yield return 0;
			}
			
			currentDistance = 0f;
			yield return new WaitForSeconds(patrolWait);
			transform.Rotate(0,180,0);
			
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			GetComponentInChildren<visionScript>().detected();
		}
	}


}
