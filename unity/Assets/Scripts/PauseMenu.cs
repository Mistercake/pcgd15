using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	GUISkin newSkin;
	
	public void initPauseMenu()
	{
		GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 50, 300, 250));
		GUI.Box(new Rect(0, 0, 300, 250), "");
	
		GameDirector otherScript = GetComponent<GameDirector>();
		
		// resume button
		if(GUI.Button(new Rect(55, 50, 180, 40), "Resume")){
			Time.timeScale = 1;
			GameDirector.gamePaused = false;
			Destroy(this);
		}
		// main menu button
		if(GUI.Button(new Rect(55, 100, 180, 40), "Main Menu")){
			Time.timeScale = 1;
			Application.LoadLevel(0);
			Destroy(this);
		}
		// quit button
		if(GUI.Button(new Rect(55, 150, 180, 40), "Quit")){
			Application.Quit();	
		}
		GUI.EndGroup();
	}

	void OnGUI (){
		GUI.skin = newSkin;
		Screen.showCursor = true;
		initPauseMenu();
	}
	
}

