using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	
	public GameObject player;
	public float moveTime = .5f;
	
	private GameObject targetPlayer;
	private float i;
	private bool transitioning;
	private bool firstTimeSeeking;
	private Vector3 startPos, endPos;
	private float zPos;

	private Material screen;

	// Use this for initialization
	void Start () {
		firstTimeSeeking = true;
		transitioning = false;
		zPos = camera.transform.position.z;

		screen = GetComponentInChildren<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		if (player && player.GetComponent<PlayerController>().isSelected()){
			trackTarget ();
		}
		else if (firstTimeSeeking){
			player = seekTarget();
			firstTimeSeeking = false;
		}
		else{
			if (transitioning){
				if (i < 1f){
					i += Time.deltaTime * 1/moveTime;
					this.transform.position = Vector3.Lerp(startPos,endPos,i);
				}
				else{
					player = targetPlayer;
					transitioning = false;
				}
			}
			else{
				targetPlayer = seekTarget();
				startPos = new Vector3(transform.position.x,transform.position.y + 2,zPos);
				endPos = new Vector3(targetPlayer.transform.position.x,targetPlayer.transform.position.y + 2,zPos);
				i = 0f;
				transitioning = true;
			}
		}
		
	}
	
	// tracks the current target
	private void trackTarget(){
		Vector3 position = player.transform.position;
		
		camera.transform.position = new Vector3(
			position.x,  
			position.y + 2,
			camera.transform.position.z
			);
	}
	
	// seeks a new target
	public GameObject seekTarget(){
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject p in players){
			if (p.GetComponent<PlayerController>().isSelected()){
				return p;
			}
		}
		return null;
	}

	public void setScreenAlpha(float i){
		screen.color = new Color (screen.color.r, screen.color.g, screen.color.b, i);
	}

	public float getScreenAlpha(){
		return screen.color.a;
	}
}