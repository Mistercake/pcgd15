using UnityEngine;
using System.Collections;

public class GuardMovement : MonoBehaviour {

	NavMeshAgent agent;
	CharacterController controller;
	Transform player;
	Animator animator;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.updatePosition = false;
		agent.updateRotation = false;
		controller = gameObject.GetComponent<CharacterController>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(player.position);
		Vector3 input = Vector3.ClampMagnitude(agent.nextPosition - transform.position, 1f);
		input.y = 0f;
		
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, input, 0.5f, 0f));
		
		controller.Move(agent.velocity*Time.deltaTime);
		animator.SetFloat("Velocity", controller.velocity.magnitude);
		
		controller.Move(Vector3.down*9.81f*Time.deltaTime);
	}
}
