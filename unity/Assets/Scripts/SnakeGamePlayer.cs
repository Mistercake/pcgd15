using UnityEngine;
using System.Collections;

public class SnakeGamePlayer : MonoBehaviour {


	public float movementSpeed;
	public BonusObjectiveController bonusObject;
	public FlashEffectController flashEffect;
	public LineDrawerController lineDrawer;

	private int direction; // 0: no direction, 1: up, 2: right, 3: down, 4: left
	private Vector3 startPosition;
	
	bool ready = false;


	void Start () {
		startPosition = transform.position;
		direction = 0;
	}
	

	void Update () {
		CheckInput ();
		MoveToDirection ();
	}

	// Checks for user input, simple as that ;)
	void CheckInput() {
		if (Input.GetButtonDown("Up") && direction != 3)
						direction = 1;
		if (Input.GetButtonDown("Right") && direction != 4)
						direction = 2;
		if (Input.GetButtonDown("Down") && direction != 1)
						direction = 3;
		if (Input.GetButtonDown("Left") && direction != 2)
						direction = 4;
	}

	// Checks for the current direction and moves player accordingly
	void MoveToDirection() {
		switch (direction) {
				case 1: // == up
						transform.position += new Vector3 (0, movementSpeed, 0) * Time.deltaTime;
						break;
				case 2: // == right
						transform.position += new Vector3 (movementSpeed, 0, 0) * Time.deltaTime;
						break;
				case 3: // == down
						transform.position -= new Vector3 (0, movementSpeed, 0) * Time.deltaTime;
						break;
				case 4: // == left
						transform.position -= new Vector3 (movementSpeed, 0, 0) * Time.deltaTime;
						break;
				}
	}

	// Called when the player fails, resets the minigame
	public void Reset() {
		flashEffect.EndScene ();
		direction = 0;
		transform.position = startPosition;
		bonusObject.DeactivateBonus ();
		lineDrawer.reset ();
	}

	// Called when the goal object is reached, disables player object
	void GoalReached() {
		direction = 0;
		// connectedObject.activate ();
		ready = true;
		gameObject.SetActive (false); // NEEDS TO BE CALLED LAST, not sure if this is the best way to disable user interaction?
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Obstacle") {
			Debug.Log("Obstacle was hit");
			Reset ();
		}
		if (other.gameObject.tag == "Goal") {
			Debug.Log("Goal was hit");
			other.gameObject.SetActiveRecursively(true);
			GoalReached ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Bonus was hit");
		bonusObject.ActivateBonus();
	}
	
	public bool IsGoalReached(){
		return ready;
	}
	
	public bool IsBonusGoalReached(){
		return bonusObject.IsActivated();
	}
}
