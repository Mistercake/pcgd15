using UnityEngine;
using System.Collections;

public class FlashEffectController : MonoBehaviour {

	public float fadeSpeed = 1.5f;
	public bool fullFade = false;
	public bool noFade = false;
	
	
	void Awake() {
		guiTexture.pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
	}
	
	void Update() {
		if (noFade) {
			EndScene();		
		}

		if (fullFade) {
			StartScene ();
		}
	}
	
	void FadeToClear() {
		guiTexture.color = Color.Lerp (guiTexture.color, Color.clear, 1.5f * Time.deltaTime);
	}
	
	void FadeToRed() {
		guiTexture.color = Color.Lerp (guiTexture.color, Color.red, fadeSpeed * Time.deltaTime);
	}
	
	void StartScene() {
		FadeToClear ();
		
		if (guiTexture.color.a <= 0.02f) {
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			fullFade = false;
		}
	}
	
	public void EndScene() {
		noFade = true;
		fullFade = false;
		guiTexture.enabled = true;
		FadeToRed ();	
		
		if (guiTexture.color.a >= 0.40f) {
			noFade = false;
			fullFade = true;
		}
	}
}
