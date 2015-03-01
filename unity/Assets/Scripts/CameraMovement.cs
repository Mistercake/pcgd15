using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform target;
	public Vector3 angle = new Vector3(-1,-1,-1);
	public float distance = 20f;
	public float FOV = 30f;
	public float lerpRate = 0.8f;

    float aDistance;
    float aFOV;
    Vector3 aAngle;

    Transform microphone;

	// Use this for initialization
	void Start () {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        microphone = transform.Find("Microphone");
		Vector3 goalPos = target.position-(angle.normalized*distance);
		transform.rotation = Quaternion.LookRotation(target.position-transform.position);
		transform.position = goalPos;
		camera.fieldOfView = FOV;
        aDistance = distance;
        aFOV = FOV;
        aAngle = angle;
		
	}
	
	// Update is called once per frame
	void Update () {
        aDistance = Mathf.Lerp(aDistance, distance, 0.1f);
        aFOV = Mathf.Lerp(aFOV, FOV, 0.1f);
        aAngle = Vector3.Lerp(aAngle, angle, 0.1f);

		lerpRate = Mathf.Clamp(lerpRate, 0, 1);
		Vector3 goalPos = target.position-(aAngle.normalized*aDistance);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position-transform.position), lerpRate);
	 	transform.position = Vector3.Lerp(transform.position, goalPos, lerpRate);
	 	camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, aFOV, lerpRate);

        microphone.position = target.position;
        microphone.rotation = Quaternion.LookRotation(transform.forward);
	}
}
