using UnityEngine;
using System.Collections;


public class movingPlatformScript : slowableObject {
	public enum moveAxis{
		horizontal, vertical
	};
	
	public bool moving;
	// controls the movement of the platform
	public float moveTime;
	// note, moveDistance can be negative or 0
	public float moveDistance;
	public float moveWait;
	// direction that the platform is moving
	public moveAxis axis;
	
	private float currentDistance;
	private float moveDirection;
	private float moveSpeed;
	private Vector3 originalPos;

	// the player's on top of the platform
	private Hashtable playerTable;
	
	// Use this for initialization
	void Start () {
		moveDirection = 1f;
		currentDistance = 0f;
		moveDistance *= 2f;

		originalPos = transform.position;
		
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {		
		moveSpeed = moveDistance / moveTime * Time.deltaTime / timeSpeed;
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.tag == "Player" || c.gameObject.tag == "Box"){
			c.gameObject.transform.parent = transform;
		}
			/*
			if (!playerTable.ContainsKey(c.gameObject.GetComponent<PlayerController>().ID)){
				playerTable.Add(c.gameObject.GetComponent<PlayerController>().ID, c.gameObject);
				print ("added");
			}
		}
		else if (c.gameObject.tag == "Box"){
			if (!playerTable.ContainsKey(4)){
				playerTable.Add(4, c.gameObject);
			}
		}*/
	}
	
	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "Player" || c.gameObject.tag == "Box"){
			c.gameObject.transform.parent = null;
		}
			/*
			if (playerTable.ContainsKey(c.gameObject.GetComponent<PlayerController>().ID)){
				playerTable.Remove(c.gameObject.GetComponent<PlayerController>().ID);
				print ("removed");
			}
		}else if (c.gameObject.tag == "Box"){
			if (playerTable.ContainsKey(4)){
				playerTable.Remove (4);
			}
		}*/
	}
	
	IEnumerator autoMove(){
		while (moving) {
			while (Mathf.Abs(currentDistance) < Mathf.Abs(moveDistance)){
				if (axis == moveAxis.horizontal){
					transform.Translate(moveSpeed * moveDirection,0,0);
					//move the players on the platform
					/*
					foreach (DictionaryEntry entry in playerTable){
						print (entry.Key);
						((GameObject)entry.Value).transform.Translate(moveSpeed * moveDirection,0,0);
					}*/
					//rigidbody.MovePosition(new Vector3(rigidbody.position.x + moveSpeed, rigidbody.position.y, rigidbody.position.z));
				}
				else{
					transform.Translate(0,moveSpeed * moveDirection,0);
					//move the players on the platform
					/*
					foreach (DictionaryEntry entry in playerTable){
						((GameObject)entry.Value).transform.Translate(0,moveSpeed * moveDirection,0);
					}*/
					//rigidbody.MovePosition(new Vector3(rigidbody.position.x, rigidbody.position.y + moveSpeed, rigidbody.position.z));
				}
				currentDistance += moveSpeed;
				yield return 0;
				
			}
			
			currentDistance = 0f;
			moveDirection *= -1;
			yield return new WaitForSeconds(moveWait);
			
		}
		if(!moving){
			yield break;
		}
	}
	
	
	public void startMove(){
		if (moving == false){
			moving = true;
			StartCoroutine ("autoMove");
		}
	}

	public void reset(){
		print ("yo");
		moving = false;
		moveDirection = 1f;
		currentDistance = 0f;
		StopCoroutine ("autoMove");
		transform.DetachChildren ();
		transform.position = originalPos;
	}
}

