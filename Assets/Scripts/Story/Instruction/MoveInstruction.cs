using UnityEngine;
using System.Collections;

public class MoveInstruction : Instruction {
	public GameObject entity;
	public float movespeed;
	public Vector2 moveDir;
	public float targetDistance;
	private float distanceMoved;

	// Use this for initialization
	void Start () {
		distanceMoved = 0;
	}
	
	public override void Enact() {
		entity.transform.Translate(new Vector3 (moveDir.x * movespeed * Time.deltaTime, moveDir.y * movespeed * Time.deltaTime, 0));
		distanceMoved += movespeed * Time.deltaTime;
		if (distanceMoved > targetDistance) {
			completed = true;
		}
	}
}
