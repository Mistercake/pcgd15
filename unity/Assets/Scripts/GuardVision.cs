using UnityEngine;
using System.Collections;

public class GuardVision : MonoBehaviour {

	public float GuardFOV = 120f;

	Transform eyes;
	Transform player;
	float alertBuffer = 0f;
	GuardAlertness guardAlert;
	Vector3 lastPlayerPosition;

	// Use this for initialization
	void Start () {
		eyes = transform.Find("CATRigPelvis/CATRigSpine1/CATRigSpine2/CATRigTorso/CATRigSpine1 1/CATRigSpine2 1/CATRigHead/Eyes");
		player = GameObject.FindGameObjectWithTag("Player").transform;
		guardAlert = gameObject.GetComponent<GuardAlertness>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 playerTarget = player.position+Vector3.up*0.5f;
		Color hitColor = Color.cyan;
		if(Vector3.Distance(playerTarget, transform.position) < 10f && Vector3.Angle(playerTarget-eyes.position, transform.forward) < GuardFOV/2){
			RaycastHit hit;
			
			if (Physics.Raycast(eyes.position, (playerTarget-eyes.position), out hit)){
				Debug.Log(hit.transform);
				if(hit.transform.tag == "Player"){
					hitColor = Color.red;
					alertBuffer += Time.deltaTime;
					if(alertBuffer > 0.2f){
						VisualContact();
					}
				}
			}else{
				alertBuffer -= Time.deltaTime;
				alertBuffer = alertBuffer < 0 ? 0f : alertBuffer;
			}
			Debug.DrawLine(eyes.position, player.position+Vector3.up*0.5f, hitColor);
			
		}else{
			Debug.DrawLine(eyes.position, eyes.position+transform.forward, Color.yellow);
		}
		if(guardAlert.GetStatus() >= GuardAlertness.STATUS_CAUTION && Vector3.Distance(playerTarget, transform.position) < 3f){
			VisualContact();
		}
		
	}
	
	public Vector3 GetLastPlayerPosition(){
		return lastPlayerPosition;
	}
	
	void VisualContact(){
		lastPlayerPosition = player.position;
		guardAlert.VisualContact();
		Debug.Log("Guard has seen you.");
	}
}
