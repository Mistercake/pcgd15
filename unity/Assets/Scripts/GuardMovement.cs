using UnityEngine;
using System.Collections;

public class GuardMovement : MonoBehaviour {

	public GuardPatrolNode targetNode;

	NavMeshAgent agent;
	CharacterController controller;
	Transform player;
	Animator animator;
	AlertSystem alert;
	float lookAroundTimer = 0f;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.updatePosition = false;
		agent.updateRotation = false;
		controller = gameObject.GetComponent<CharacterController>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		animator = gameObject.GetComponent<Animator>();
		alert = GameObject.FindGameObjectWithTag("AlertSystem").GetComponent<AlertSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(alert.GetStatus()){
			case AlertSystem.STATUS_CLEAR:
				agent.speed = 1;
				agent.SetDestination(targetNode.transform.position);
				if(agent.remainingDistance <= agent.stoppingDistance){
					lookAroundTimer += 2f;
					targetNode = targetNode.nextNode;
				}
				break;
			case AlertSystem.STATUS_ALERT:
				agent.speed = 2.5f;
				agent.SetDestination(player.position);
				break;
		}
		
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
