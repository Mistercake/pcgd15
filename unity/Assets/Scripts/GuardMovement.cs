using UnityEngine;
using System.Collections;

public class GuardMovement : MonoBehaviour {

	public GuardPatrolNode targetNode;

	NavMeshAgent agent;
	CharacterController controller;
	Transform player;
	Animator animator;
	AlertSystem alert;
	GuardAlertness guardAlert;
	GuardVision vision;
	float lookAroundTimer = 0f;
	float defaultStoppingDistance = 1f;
	
	bool dead = false;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.updatePosition = false;
		agent.updateRotation = false;
		controller = gameObject.GetComponent<CharacterController>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		animator = gameObject.GetComponent<Animator>();
		guardAlert = gameObject.GetComponent<GuardAlertness>();
		vision = gameObject.GetComponent<GuardVision>();
		alert = GameObject.FindGameObjectWithTag("AlertSystem").GetComponent<AlertSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(!dead){
	
			CheckAlertStatus();
		
			Vector3 input = Vector3.ClampMagnitude(agent.nextPosition - transform.position, 1f);
			input.y = 0f;
			
			if(lookAroundTimer == 0){
				transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, input, 0.5f, 0f));
				controller.Move(agent.velocity*Time.deltaTime);
			}
			
			animator.SetFloat("Velocity", controller.velocity.magnitude);
			controller.Move(Vector3.down*9.81f*Time.deltaTime);
			
			lookAroundTimer -= Time.deltaTime;
			lookAroundTimer = Mathf.Clamp(lookAroundTimer, 0f, 10f);
		}
	}
	
	void CheckAlertStatus(){
		
		switch(guardAlert.GetStatus()){
			case GuardAlertness.STATUS_CLEAR:
				agent.speed = 1;
				agent.SetDestination(targetNode.transform.position);
				if(agent.remainingDistance <= agent.stoppingDistance){
					lookAroundTimer += 2f;
					targetNode = targetNode.nextNode;
				}
				break;
			case GuardAlertness.STATUS_ALERT:
				agent.speed = 2.5f;
				if(alert.GetStatus() != AlertSystem.STATUS_ALERT){
					Transform button = GetClosestAlertButton();
					agent.stoppingDistance = 0.5f;
					agent.SetDestination(button.position);
					if(Vector3.Distance(transform.position, button.position) <= 1f){
						alert.Alert(vision.GetLastPlayerPosition());
					}
				}else{
					agent.stoppingDistance = defaultStoppingDistance;
					agent.SetDestination(alert.GetLastPlayerPosition());
				}
				break;
		}
	
	}
	
	Transform GetClosestAlertButton(){
		Transform result = null;
		foreach(GameObject button in GameObject.FindGameObjectsWithTag("AlertButton")){
			if(result == null || Vector3.Distance(transform.position, button.transform.position) < Vector3.Distance(transform.position, result.position)){
				result = button.transform;
			}	
		}
		return result;
	}
	
	public void Die(){
		dead = true;
		animator.SetTrigger("Die");
	}
}
