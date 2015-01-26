using UnityEngine;
using System.Collections;

public class SecurityCamera : MonoBehaviour {
	
	public float pitch = 10f;
	public float RotationRange = 90f;
	public float RotationRate = 50f;
	public float CameraFOV = 45f;
	Transform cameraProp;
	Transform lens;
	Transform player;
	Transform light;
	float alertBuffer = 0f;

	// Use this for initialization
	void Start () {
		cameraProp = transform.Find("Camera");
		cameraProp.RotateAround(cameraProp.position, cameraProp.right, pitch);
		lens = cameraProp.Find("Lens");
		light = cameraProp.Find("Light");
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraPos = cameraProp.TransformPoint(Vector3.zero);
		cameraProp.RotateAround(cameraPos, Vector3.up, RotationRate*Time.deltaTime); 
		if(Vector3.Angle(transform.forward, lens.forward) > (RotationRange/2)){
			RotationRate *= -1;
		}
		
		Vector3 playerTarget = player.position+Vector3.up*0.5f;
		Color hitColor = Color.cyan;
		if(Vector3.Distance(playerTarget, transform.position) < 10f && Vector3.Angle(playerTarget-lens.position, lens.forward) < CameraFOV){
			RaycastHit hit;
			
			if (Physics.Raycast(lens.position, (playerTarget-lens.position), out hit)){
				Debug.Log(hit.transform);
				if(hit.transform.tag == "Player"){
					hitColor = Color.red;
					alertBuffer += Time.deltaTime;
					if(alertBuffer > 1.5f){
						GameObject.FindGameObjectWithTag("AlertSystem").GetComponent<AlertSystem>().Alert();
					}
					Debug.Log("Security Camera Alert");
				}
			}
			Debug.DrawLine(lens.position, player.position+Vector3.up*0.5f, hitColor);
			
		}
		light.GetComponent<LensFlare>().color = hitColor;
	}
}
