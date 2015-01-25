using UnityEngine;
using System.Collections;

public class Terminal : MonoBehaviour {

	bool playerNear = false;
	Transform playerMark;
	PlayerMovement player;

	// Use this for initialization
	void Start () {
		playerMark = transform.Find("PlayerMark");
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerNear && Input.GetButton("Action")){
			//player.position = Vector3.Lerp(player.position, playerMark.position, 0.1f);
			//player.rotation = Quaternion.LookRotation(Vector3.RotateTowards(player.forward, playerMark.forward, 0.1f, 0f));
			player.SetInput(Vector3.ClampMagnitude(playerMark.position-player.transform.position, 1f));
			player.SetFaceTarget(transform.position);
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			playerNear = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			playerNear = false;
		}
	}
}
