using UnityEngine;
using System.Collections;

public class BarsMinigame_CollissionDetector : MonoBehaviour {

	public BarsMinigame_MasterAlarmbarController MasterAlarmBar;
	public ProgressBarController ProgressBar;
	public FlashEffectController FlashEffect;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Alarm_bar") {
			Reset ();
		}
		if (other.tag == "Goal_bar") {
			GoalReached();
		}

	}

	void Reset() {
		Debug.Log ("Reset was called");
		MasterAlarmBar.Reset ();
		ProgressBar.Reset ();
		FlashEffect.EndScene ();
	}

	void GoalReached() {
        FindObjectOfType<BarsGame>().GoalReached();
		Debug.Log ("GoalReached was called");
		MasterAlarmBar.StopAllBars ();
		ProgressBar.moving = false;
	}
}
