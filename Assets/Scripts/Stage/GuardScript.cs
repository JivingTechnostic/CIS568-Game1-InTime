using UnityEngine;
using System.Collections;

public class GuardScript : MonoBehaviour {
	public float patrolTime = 4;
	public float rightDistance = 5;
	public float leftDistance = 5;
	public float patrolWait = 3f;
	
	private float patrolDistance;
	private float currentDistance;
	private float currentDirection;
	private float patrolSpeed;

	//time control
	private float timeSpeed = 1f; // factor by which movement will be slowed by
	private Hashtable table;

	// Use this for initialization
	void Start () {
		currentDistance = 0f;
		patrolDistance = rightDistance + leftDistance;

		table = new Hashtable ();

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
