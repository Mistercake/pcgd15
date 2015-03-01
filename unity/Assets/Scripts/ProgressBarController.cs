using UnityEngine;
using System.Collections;

public class ProgressBarController : MonoBehaviour {

	public float speed;
	public bool moving;

	
	// Use this for initialization
	void Start () {
		moving = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (moving) {
			float scaleMe = transform.localScale.x;
			scaleMe += speed * Time.deltaTime;
			transform.localScale = new Vector3 (scaleMe, 1, 1);
		}
	}

	public void Reset() {
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
