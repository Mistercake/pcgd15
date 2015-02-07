using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public bool OpenInwards = false;
	public bool StartOpen = false;
	Animator animator;
	bool open = false;
	
	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
		if(StartOpen) Toggle();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			this.Toggle();
		}
	}
	
	public void Toggle(){
		if(!open){
			if(OpenInwards){
				this.OpenIn();
			}else{
				this.OpenOut();
			}
		}else{
			this.Close();
		}
	}
	
	public void OpenOut(){
		if(!open){
			animator.SetTrigger("OpenOut");
			open = true;
		}
	}
	
	public void OpenIn(){
		if(!open){
			animator.SetTrigger("OpenIn");
			open = true;
		}
	}
	
	public void Close(){
		animator.SetTrigger("Close");
		open = false;
	}
}
