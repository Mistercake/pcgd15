using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {
	
	Animator animator;
	PlayerMovement player;
    Occluder occluder;
	
	// Use this for initialization
	void Start () {
        occluder = GameObject.FindObjectOfType<Occluder>();
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.IsDead())
        {
            Invoke("SlideOut", 1f);
        }
	}
	
	void SlideOut(){
		animator.SetTrigger("SlideOut");
        occluder.TransitionOut();
		Invoke("Restart", 1.3f);
		
	}
	
	void Restart(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void Win(){
		animator.SetTrigger("Victory");
		Invoke("SlideOut", 1f);
	}
}
