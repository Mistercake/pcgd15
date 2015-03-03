using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public bool OpenInwards = false;
	public bool StartOpen = false;
	Animator animator;
	bool open = false;

    AudioSource soundOpen;
    AudioSource soundClose;
	
	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
		if(StartOpen) Toggle();

        AudioSource[] audios = GetComponents<AudioSource>();
        Debug.Log(audios.Length);
        soundOpen = audios[0];
        soundClose = audios[1];
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

    public void OpenNoise()
    {
        soundOpen.Play();
    }

    public void CloseNoise()
    {
        soundClose.Play();
    }
}
