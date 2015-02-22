using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {
	
	Animator animator;
	PlayerMovement player;
<<<<<<< HEAD:unity/Assets/Scripts/GameDirector.cs
    Occluder occluder;
	
=======
	public static bool gamePaused = false;
	Component pauseMenu = null;

>>>>>>> origin/master:unity/Assets/GameDirector.cs
	// Use this for initialization
	void Start () {
        occluder = GameObject.FindObjectOfType<Occluder>();
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD:unity/Assets/Scripts/GameDirector.cs
        if (player.IsDead())
        {
            Invoke("SlideOut", 1f);
        }
=======
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
>>>>>>> origin/master:unity/Assets/GameDirector.cs
	}
	
	void SlideOut(){
		animator.SetTrigger("SlideOut");
<<<<<<< HEAD:unity/Assets/Scripts/GameDirector.cs
        occluder.TransitionOut();
		Invoke("Restart", 1.3f);
		
=======
		Invoke("NextLevel", 1.3f);

>>>>>>> origin/master:unity/Assets/GameDirector.cs
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
