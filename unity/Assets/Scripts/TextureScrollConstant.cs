using UnityEngine;
using System.Collections;

public class TextureScrollConstant : MonoBehaviour {

    Material material;
    public Vector2 floatRate;

	// Use this for initialization
	void Start () {
        material = gameObject.renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
        material.mainTextureOffset += floatRate * Time.deltaTime;
	}
}
