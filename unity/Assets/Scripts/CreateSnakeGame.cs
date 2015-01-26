using UnityEngine;
using System.Collections;

public class CreateSnakeGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Instantiate(Resources.Load("SnakeGame"), Vector3.up*10, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
