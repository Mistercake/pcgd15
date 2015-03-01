using UnityEngine;
using System.Collections;

public class CameraSettingsVolume : MonoBehaviour {

    public Vector3 angle = new Vector3(-1,-1,-1);
    public float distance;
    public float FOV;
    public float lerpRate;

    Vector3 oAngle;
    float oDistance;
    float oFOV;
    float oLerpRate;

    bool active = false;
    CameraMovement cam;

    float i = 0;

	// Use this for initialization
	void Start () {
        cam = GameObject.FindObjectOfType<CameraMovement>();
	}


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            oAngle = cam.angle;
            cam.angle = angle;
            oDistance = cam.distance;
            cam.distance = distance;
            oFOV = cam.FOV;
            cam.FOV = FOV;
            oLerpRate = cam.lerpRate;
            cam.lerpRate = lerpRate;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cam.angle = oAngle;
            cam.distance = oDistance;
            cam.FOV = oFOV;
            cam.lerpRate = oLerpRate;
        }
    }
}
