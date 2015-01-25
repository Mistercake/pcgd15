using UnityEngine;
using System.Collections;

public class TerminalScreenScroll : MonoBehaviour {
	
	float prev = 0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > prev){
			gameObject.renderer.material.mainTextureOffset -= new Vector2(0,0.1f)*Random.value;
			prev = Time.time + Random.value*0.5f;
		} 
	}
}
