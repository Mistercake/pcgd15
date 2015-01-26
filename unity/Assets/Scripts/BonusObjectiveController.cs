using UnityEngine;
using System.Collections;

public class BonusObjectiveController : MonoBehaviour {

	public Sprite activeSprite;
	public Sprite deactiveSprite;

	public void ActivateBonus() {
		gameObject.GetComponent<SpriteRenderer>().sprite = activeSprite;
	}

	public void DeactivateBonus() {
		gameObject.GetComponent<SpriteRenderer>().sprite = deactiveSprite;
	}

}
