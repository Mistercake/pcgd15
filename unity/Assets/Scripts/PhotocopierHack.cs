using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhotocopierHack : MonoBehaviour {

	Transform pile;
	Transform shooter;
	Transform spreading;
    List<Collider> visitors;
	
	bool piling = false;
    float throttle = 0f;

	// Use this for initialization
	void Start () {
        visitors = new List<Collider>();
		pile = transform.Find("PaperPile");
		shooter = transform.Find("PapersShooting");
		spreading = transform.Find("PapersSpreading");
	}
	
	// Update is called once per frame
	void Update () {
		if(piling){
			pile.localScale = Vector3.Lerp(pile.localScale, Vector3.one*2f, 0.1f);
            if (Time.time - throttle > 1f)
            {
                foreach (Collider enemy in Physics.OverlapSphere(transform.position, 10f, (1 << 9))) // layer "Enemies"
                {
                    if (enemy.tag == "Guard" && !visitors.Contains(enemy))
                    {
                        visitors.Add(enemy);
                        enemy.GetComponent<GuardMovement>().SetInvestigation(transform.position);
                    }
                }
                throttle = Time.time;
            }
		}
	}
	
	void Hack(){
		shooter.gameObject.SetActive(true);
		spreading.gameObject.SetActive(true);
		Invoke("Pile", 0.5f);
	}
	
	void Pile(){
		pile.gameObject.SetActive(true);
		pile.localScale = Vector3.zero;
		piling = true;
	}
}
