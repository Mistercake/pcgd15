using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarsMinigame_MasterAlarmbarController : MonoBehaviour {

	public GameObject playerBar;

	private ArrayList allChilds = new ArrayList();
	private int currentIndex;


	void Start() {
		foreach (Transform child in transform)
		{
			allChilds.Add(child);
		}
		currentIndex = 0;

		AlignPlayerBarByIndex ();
	}

	void Update() {
		CheckInput ();
		AlignPlayerBarByIndex ();
	}

	// Can be rewritten to call any input-handler, mind the currentIndex correction at the end!!
	void CheckInput() {

		if (Input.GetAxis("Right") > 0.1f) {
			currentIndex++;	
		}

		if (Input.GetAxis("Right") < -0.1f) {
			currentIndex--;	
		}

		if (currentIndex < 0) {
            currentIndex = transform.childCount - (0 - currentIndex);	
		}
		if (currentIndex > transform.childCount-1) {
			currentIndex = 0 + (currentIndex-transform.childCount);	
		}

		// this will be replaced by current action-button
		if (Input.GetKeyDown(KeyCode.Space)) {
			PushAlarmBarInIndex(currentIndex);
		}
	}

	public void Reset() {
		foreach (Transform child in allChilds)
		{
			child.transform.localScale = new Vector3 (1, 1, 1);

		}
	}

	public void StopAllBars() {
		foreach (Transform child in allChilds)
		{
			child.GetComponentInChildren<AlarmBarController>().moving = false;
			
		}
	}

	void AlignPlayerBarByIndex() {
		Transform child = (Transform) allChilds [currentIndex];
		float indexPosition = child.transform.position.x + 0.07f; // Alignment correction, strange shit...
		playerBar.transform.position = new Vector3 (indexPosition, playerBar.transform.position.y, playerBar.transform.position.z);
	}

	void PushAlarmBarInIndex(int index) {
		Transform child = (Transform) allChilds [index];
		Vector3 tempScale = child.transform.localScale;
		tempScale -= new Vector3 (0, tempScale.y/2, 0);
		if (tempScale.y < 1) {
			tempScale = new Vector3(1, 1, 1);		
		}
		child.transform.localScale = tempScale;
	}
}
