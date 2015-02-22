using UnityEngine;
using System.Collections;

public class GuardAlertness : MonoBehaviour {

	public const int STATUS_CLEAR = 0;
	public const int STATUS_CAUTION = 1;
	public const int STATUS_ALERT = 2;
	
	int currentStatus = STATUS_CLEAR;
	float statusTime = 0f;
	float lastSeen = 0f;
	
	AlertSystem globalAlert;
	GuardVision vision;

	// Use this for initialization
	void Start () {
		vision = gameObject.GetComponent<GuardVision>();
		globalAlert = GameObject.FindGameObjectWithTag("AlertSystem").GetComponent<AlertSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentStatus == STATUS_ALERT && statusTime > 99){
			currentStatus = STATUS_CAUTION;
		}
		
		statusTime += Time.deltaTime;
		lastSeen += Time.deltaTime;
	}
	
	public void Alert(){
		currentStatus = STATUS_ALERT;
		statusTime = 0f;
	}
	
	public void VisualContact(){
		lastSeen = 0f;
		Alert();
		if(globalAlert.GetStatus() == AlertSystem.STATUS_ALERT){
			globalAlert.Alert(vision.GetLastPlayerPosition());
		}
	}
	
	public int GetStatus(){
		return currentStatus;
	}

    public void DropToCaution()
    {
        currentStatus = STATUS_CAUTION;
        GetComponent<GuardMovement>().SelectTargetNode();
    }
}
