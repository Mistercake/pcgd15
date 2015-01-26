using UnityEngine;
using System.Collections;

public class Terminal : MonoBehaviour {

	public GameObject target;
	public string methodName;

	bool playerNear = false;
	Transform playerMark;
	PlayerMovement player;
	
	bool used = false;

	// Use this for initialization
	void Start () {
		playerMark = transform.Find("PlayerMark");
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerNear && Input.GetButtonDown("Action")){
			/*used = true;
			player.SetMovement(false);*/
			target.SendMessage(methodName);
		}
		/*
		if(used){
			if(Vector3.Distance(player.transform.position, playerMark.position) > 0.1f){
				player.SetInput(Vector3.ClampMagnitude(playerMark.position-player.transform.position, 1f));
				player.SetFaceTarget(transform.position);
			}
		}*/
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
