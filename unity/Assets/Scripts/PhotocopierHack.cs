using UnityEngine;
using System.Collections;

public class PhotocopierHack : MonoBehaviour {

	Transform pile;
	Transform shooter;
	Transform spreading;
	
	bool piling = false;

	// Use this for initialization
	void Start () {
		pile = transform.Find("PaperPile");
		shooter = transform.Find("PapersShooting");
		spreading = transform.Find("PapersSpreading");
	}
	
	// Update is called once per frame
	void Update () {
		if(piling){
			pile.localScale = Vector3.Lerp(pile.localScale, Vector3.one*2f, 0.1f);
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
