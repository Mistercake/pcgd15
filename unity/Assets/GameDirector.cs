using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {
	
	Animator animator;
	PlayerMovement player;
	public static bool gamePaused = false;
	Component pauseMenu = null;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.IsDead()) Invoke("SlideOut", 1f);
		// show pause menu when esc pressed
		if (Input.GetKeyDown ("escape")) {
			if (!gamePaused) {
				Time.timeScale = 0;
				gamePaused = true;
				pauseMenu = gameObject.AddComponent("PauseMenu");
			} else {
				Time.timeScale = 1;
				gamePaused = false;
				Destroy (pauseMenu);
			}
		}
	}
	
	void SlideOut(){
		animator.SetTrigger("SlideOut");
		Invoke("NextLevel", 1.3f);

	}
	
	void Restart(){
		Application.LoadLevel(Application.loadedLevel);
	}

	void NextLevel(){
		Application.LoadLevel(Application.loadedLevel+1);
	}

	public void Win(){
		animator.SetTrigger("Victory");
		Invoke("SlideOut", 1f);
	}
}
