using UnityEngine;
using System.Collections;

public class AlarmBarController : MonoBehaviour {


	public float defaultSpeed = 2f;
	public float lastRandomization;
	public bool moving;

	private float speed;


	// Use this for initialization
	void Start () {
		moving = true;
		speed = 2f;
		lastRandomization = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (moving) {
			if (Time.time > lastRandomization + 3) {
				RandomizeSpeed ();
				lastRandomization = Time.time;
			}
			float scaleMe = transform.localScale.y;
			scaleMe += speed * Time.deltaTime;
			transform.localScale = new Vector3 (1, scaleMe, 1);
		}
	}

	void RandomizeSpeed() {
		speed = speed * Random.value + Random.value + 0.01f;

		Debug.Log ("Speed was randomised to: " + speed);
	}

	public void Reset() {
		transform.localScale = new Vector3 (1, 1, 1);
		speed = defaultSpeed;
	}

}
