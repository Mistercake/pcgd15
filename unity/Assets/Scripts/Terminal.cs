using UnityEngine;
using System.Collections;

public class Terminal : MonoBehaviour {

	public GameObject target;
	public string targetMethodName;
	
	public GameObject bonusTarget;
	public string bonusTargetMethodName;
	
	public string MiniGameName;

	bool playerNear = false;
	Transform playerMark;
	PlayerMovement player;
	
	bool used = false;

	// Use this for initialization
	void Start () {
		playerMark = transform.Find("PlayerMark");
		player = GameObject.FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerNear && Input.GetButtonDown("Action")){
			Use();
		}
	}
	
	void Use(){
		used = true;
		player.SetMovement(false);
		
		GameObject minigame = (GameObject) Instantiate(Resources.Load("SnakeGame"), Vector3.zero, Quaternion.identity);
		MiniGame game = minigame.GetComponent(typeof(MiniGame)) as MiniGame;
		game.SetTerminal(this);
    }
    
    void UnUse(){
    	used = false;
    	player.SetMovement(true);
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
	
	public void MiniGameQuit(){
		player.SetMovement(true);
	}
	
	public void MiniGameFinished(bool IsBonusGoalReached){
		target.SendMessage(targetMethodName);
		if(IsBonusGoalReached) bonusTarget.SendMessage(bonusTargetMethodName);
		player.SetMovement(true);
	}
	
	
}
