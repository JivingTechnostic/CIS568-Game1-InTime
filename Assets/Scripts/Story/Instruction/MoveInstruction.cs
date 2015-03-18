using UnityEngine;
using System.Collections;

public class MoveInstruction : Instruction {
	public GameObject entity;
	public Vector2 moveDir;
	public float targetDistance;
	private float distanceMoved;
	private float moveSpeed;
	public bool doubleTime;

	// Use this for initialization
	void Start () {
		distanceMoved = 0;
		moveSpeed = 2;
	}
	
	public override void Enact() {
		Vector3 baseMove = new Vector3 (moveDir.x * moveSpeed * Time.deltaTime, moveDir.y * moveSpeed * Time.deltaTime, 0);
		if (doubleTime) {
			baseMove *= 2;
		}
		entity.transform.Translate(baseMove);
		distanceMoved += baseMove.magnitude;
		if (distanceMoved > targetDistance) {
			completed = true;
		}
	}
}
