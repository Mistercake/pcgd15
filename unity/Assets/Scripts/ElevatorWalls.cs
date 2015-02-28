using UnityEngine;
using System.Collections;

public class ElevatorWalls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.rigidbody.velocity += (Vector2.zero - (Vector2)coll.transform.position);

    }
}
