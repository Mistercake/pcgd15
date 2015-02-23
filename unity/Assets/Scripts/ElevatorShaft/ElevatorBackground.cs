using UnityEngine;
using System.Collections;

public class ElevatorBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down*Time.deltaTime*20);
        if (transform.position.y <= -9)
        {
            transform.position = Vector3.up*6;
        }
	}

    Vector3 SelectWarpPosition()
    {
        Transform target = transform;
        foreach (Transform piece in GameObject.FindObjectOfType<ElevatorBackground>().transform)
        {
            if (piece.position.y > target.position.y) target = piece;
        }
        Vector3 result = target.position;
        result.y += 3;
        return result;
    }
}
