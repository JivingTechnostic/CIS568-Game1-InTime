using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sliderScript : MonoBehaviour {
	private bool selected = false;
	private GameObject arrow;

	// Use this for initialization
	void Start () {
		arrow = transform.FindChild ("arrow").gameObject;
		arrow.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (selected){
			arrow.SetActive (true);
		}
		else{
			arrow.SetActive (false);
		}
	}

	public void setSelected(bool b){
		selected = b;
	}
}
