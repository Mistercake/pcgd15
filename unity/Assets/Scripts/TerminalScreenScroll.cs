using UnityEngine;
using System.Collections;

public class TerminalScreenScroll : MonoBehaviour {
	
	float prev = 0f;
    Material material;
	
	// Use this for initialization
	void Start () {
        material = new Material(gameObject.renderer.material);
        gameObject.renderer.material = material;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > prev){
			material.mainTextureOffset -= new Vector2(0,0.1f)*Random.value;
			prev = Time.time + Random.value*0.5f;
		}
	}

    public void setColor(Color c)
    {
        material.color = c;
    }
}
