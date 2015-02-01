using UnityEngine;
using System.Collections;

public class PunchVolume : MonoBehaviour {

	void OnTriggerStay(Collider other){
		if(other.tag == "Player"){
			other.GetComponent<PlayerMovement>().Die();
		}
	}
}
