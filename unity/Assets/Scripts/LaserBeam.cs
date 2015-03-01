using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

    public Vector3 offset;
    Vector3 endPoint;
    Vector3 startPoint;
    public float startPhase = 0;
    public float speed = 1;
    float currentPhase;
    float timer;

	// Use this for initialization
	void Start () {
        startPoint = transform.position;
        endPoint = startPoint + offset;
        currentPhase = startPhase;
        transform.position = Vector3.Lerp(startPoint, endPoint, currentPhase);
        timer = startPhase;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.Lerp(startPoint, endPoint, currentPhase);
        currentPhase = Mathf.PingPong(timer, 1);
        if (Vector3.Distance(startPoint, endPoint) != 0)
        {
            timer += (speed / Vector3.Distance(startPoint, endPoint)) * Time.deltaTime;
        }
	}
}
