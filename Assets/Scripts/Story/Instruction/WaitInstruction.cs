using UnityEngine;
using System.Collections;

public class WaitInstruction : Instruction {
	public float delay;
	
	public override void Enact() {
		delay -= Time.deltaTime;
		if (delay < 0) {
			completed = true;
		}
	}
}
