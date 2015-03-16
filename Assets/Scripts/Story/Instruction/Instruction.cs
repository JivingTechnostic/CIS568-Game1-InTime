using UnityEngine;
using System.Collections;

public class Instruction : MonoBehaviour {
	protected bool completed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame while the instruction is active.
	public virtual void Enact () {
	
	}

	public bool isComplete() {
		return completed;
	}
}
