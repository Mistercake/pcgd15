using UnityEngine;
using System.Collections;

public class ElevatorPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 input = Vector2.right*Input.GetAxis("Horizontal");
        rigidbody2D.velocity = Vector2.up * rigidbody2D.velocity.y + input*5;

        if (Input.GetButtonDown("Action"))
        {
            rigidbody2D.velocity += Vector2.up * 5;
        }
	}
}
