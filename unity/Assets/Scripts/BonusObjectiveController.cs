using UnityEngine;
using System.Collections;

public class BonusObjectiveController : MonoBehaviour {

	public Sprite activeSprite;
	public Sprite deactiveSprite;
	
	bool activated = false;

	public void ActivateBonus() {
		gameObject.GetComponent<SpriteRenderer>().sprite = activeSprite;
		activated = true;
	}

	public void DeactivateBonus() {
		gameObject.GetComponent<SpriteRenderer>().sprite = deactiveSprite;
		activated = false;
	}
	
	public bool IsActivated(){
		return activated;
	}

}
