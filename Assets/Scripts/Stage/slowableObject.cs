using UnityEngine;
using System.Collections;

public abstract class slowableObject : MonoBehaviour {
	protected float timeSpeed = 1f; // factor by which movement will be slowed by
	protected Hashtable table;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void slowDown(int ID, float factor){
		if (table == null)
			table = new Hashtable ();

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
