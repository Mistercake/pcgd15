using UnityEngine;
using System.Collections;

public class ResetButtonController : MonoBehaviour {

	void Update() {
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel(0);	
		}
	}
	
	void OnMouseUp() {
		Application.LoadLevel(0);
	}
}