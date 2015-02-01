using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform target;
	public Vector3 angle = new Vector3(-1,-1,-1);
	public float distance = 20f;
	public float FOV = 30f;
	public float lerpRate = 0.8f;

	// Use this for initialization
	void Start () {
		Vector3 goalPos = target.position-(angle.normalized*distance);
		transform.rotation = Quaternion.LookRotation(target.position-transform.position);
		transform.position = goalPos;
		camera.fieldOfView = FOV;
		
	}
	
	// Update is called once per frame
	void Update () {
		lerpRate = Mathf.Clamp(lerpRate, 0, 1);
		Vector3 goalPos = target.position-(angle.normalized*distance);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position-transform.position), lerpRate);
	 	transform.position = Vector3.Lerp(transform.position, goalPos, lerpRate);
	 	camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, FOV, lerpRate);
	}
}
