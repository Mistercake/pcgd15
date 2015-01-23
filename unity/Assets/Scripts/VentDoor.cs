using UnityEngine;
using System.Collections;

public class VentDoor : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			animator.SetTrigger("OpenRight");
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			animator.SetTrigger("Close");
		}
	}
}
