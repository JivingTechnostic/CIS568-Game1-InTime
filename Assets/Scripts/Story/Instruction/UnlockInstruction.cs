using UnityEngine;
using System.Collections;

public class UnlockInstruction : Instruction {
	public GameObject unlockAlertPrefab;
	private bool isActive;

	public UnlockAlertScript.AlertType type;
	public ControllerScript.Area areaUnlock;
	public ControllerScript.Stage stageUnlock;
	public ControllerScript.Item itemUnlock;
	UnlockAlertScript alertScript;

	// Use this for initialization
	void Start () {
		isActive = false;
	}
	
	public override void Enact() {
		if (!isActive) {
			alertScript = (Instantiate (unlockAlertPrefab) as GameObject).GetComponent<UnlockAlertScript>();
			switch(type) {
			case UnlockAlertScript.AlertType.AREA:
				alertScript.setAreaUnlock(areaUnlock);
				break;
			case UnlockAlertScript.AlertType.STAGE:
				alertScript.setStageUnlock(stageUnlock);
				break;
			case UnlockAlertScript.AlertType.ITEM:
				alertScript.setItemUnlock(itemUnlock);
				break;
			}
			isActive = true;
		}
		
		if (isActive && alertScript == null) {
			completed = true;
		}
	}
}
