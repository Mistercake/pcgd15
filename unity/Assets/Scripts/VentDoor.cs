using UnityEngine;
using System.Collections;

public class VentDoor : MonoBehaviour {

	Animator animator;
    LensFlare light;
	public bool locked;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
        light = transform.Find("Light").GetComponent<LensFlare>();
        if (!locked) light.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other){
		if(!locked && other.tag == "Player"){
			animator.SetTrigger("OpenRight");
		}
	}
	
	void OnTriggerExit(Collider other){
		if(!locked && other.tag == "Player"){
			animator.SetTrigger("Close");
		}
	}

    public void Unlock()
    {
        locked = false;
        light.gameObject.SetActive(false);
    }
}
