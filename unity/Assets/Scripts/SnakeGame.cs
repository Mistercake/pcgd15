using UnityEngine;
using System.Collections;

public interface MiniGame{
	bool IsGoalReached();
	bool IsBonusGoalReached();
	void SetTerminal(Terminal terminal);
}

public class SnakeGame : MonoBehaviour, MiniGame {

	Terminal activator;
	SnakeGamePlayer player;
	
	bool ready = false;
	bool bonus = false;
    
	// Use this for initialization
	void Start () {
		player = transform.Find("Player").gameObject.GetComponent<SnakeGamePlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsBonusGoalReached() && player.IsBonusGoalReached()){
			bonus = true;
		}
		if(!IsGoalReached() && player.IsGoalReached()){
			ready = true;
		}
		if(IsGoalReached()){
			Debug.Log("End Snake Game");
			activator.MiniGameFinished(IsBonusGoalReached());
			Destroy(gameObject);
		}
		if(Input.GetButton("Cancel")){
			activator.MiniGameQuit();
			Destroy(gameObject);
		}
	}
	
	public bool IsGoalReached(){
		return ready;
	}
	
	public bool IsBonusGoalReached(){
		return bonus;
	}
	
	public void SetTerminal(Terminal terminal){
		activator = terminal;
	}
}
