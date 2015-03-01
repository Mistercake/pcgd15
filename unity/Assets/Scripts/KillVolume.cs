using UnityEngine;
using System.Collections;

public class KillVolume : MonoBehaviour {

	public void OnTriggerEnter(Collider other){
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().Die();
        }
        if (other.tag == "Guard") {
            other.GetComponent<GuardMovement>().Die();
        }
    }
}
