using UnityEngine;
using System.Collections;

public class VentDoor : MonoBehaviour {

	Animator animator;
	public bool locked;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other){
		if(!locked && other.tag == "Player"){
			animator.SetTrigger("OpenRight");
		}
	}
	
	void OnTriggerExit(Collider other){
		if(!locked && other.tag == "Player"){
			animator.SetTrigger("Close");
		}
	}
}
