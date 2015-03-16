using UnityEngine;
using System.Collections;


public class movingPlatformScript : slowableObject {
	public enum moveAxis{
		horizontal, vertical
	};
	
	private bool active;
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
	
	// Use this for initialization
	void Start () {
		moveDirection = 1f;
		currentDistance = 0f;
		moveDistance *= 2f;
		
		active = false;
	}
	
	// Update is called once per frame
	void Update () {		
		moveSpeed = moveDistance / moveTime * Time.deltaTime / timeSpeed;
	}
	
	IEnumerator autoMove(){
		while (true) {
			while (Mathf.Abs(currentDistance) < Mathf.Abs(moveDistance)){
				if (axis == moveAxis.horizontal){
					transform.Translate(moveSpeed * moveDirection,0,0);
					//rigidbody.MovePosition(new Vector3(rigidbody.position.x + moveSpeed, rigidbody.position.y, rigidbody.position.z));
				}
				else{
					transform.Translate(0,moveSpeed * moveDirection,0);
					//rigidbody.MovePosition(new Vector3(rigidbody.position.x, rigidbody.position.y + moveSpeed, rigidbody.position.z));
				}
				currentDistance += moveSpeed;
				yield return 0;
				
			}
			
			currentDistance = 0f;
			moveDirection *= -1;
			yield return new WaitForSeconds(moveWait);
			
		}
	}
	
	
	public void startMove(){
		if (active == false)
			StartCoroutine (autoMove ());
		active = true;
	}
}

