using UnityEngine;
using System.Collections;

public class Occluder : MonoBehaviour {

    RectTransform hole;
    RectTransform top;
    RectTransform right;
    RectTransform bottom;
    RectTransform left;

    float duration = 1f;
    float i = 0f;
    bool transitioning = false;

    Vector3 startScale;
    Vector3 targetScale;

	// Use this for initialization
	void Start () {
        hole = (RectTransform) transform.Find("Hole");
        hole.localScale = Vector3.zero;

        top = (RectTransform) transform.Find("Top");
        right = (RectTransform) transform.Find("Right");
        bottom = (RectTransform) transform.Find("Bottom");
        left = (RectTransform) transform.Find("Left");

        TransitionIn();
	}
	
	// Update is called once per frame
	void Update () {
        hole.anchoredPosition = new Vector2(Screen.width / 2, Screen.height / 2);

        hole.localScale = Vector3.Lerp(startScale, targetScale, i);
        i += Time.deltaTime*(1/duration);
        i = Mathf.Clamp(i, 0, 1);
        if (i >= 1) transitioning = false;

        top.anchorMin = Camera.main.ScreenToViewportPoint(new Vector2(0, hole.anchoredPosition.y + hole.rect.height*hole.localScale.y / 2));

        right.anchorMin = Camera.main.ScreenToViewportPoint(new Vector2(hole.anchoredPosition.x + hole.rect.width*hole.localScale.x / 2, 0));

        bottom.anchorMax = Camera.main.ScreenToViewportPoint(new Vector2(Screen.width, hole.anchoredPosition.y - hole.rect.height*hole.localScale.y / 2));

        left.anchorMax = Camera.main.ScreenToViewportPoint(new Vector2(hole.anchoredPosition.x - hole.rect.width*hole.localScale.x / 2, Screen.height));
	}

    public void TransitionIn()
    {
        if (transitioning) return;
        startScale = hole.localScale;
        targetScale = Vector3.one * ((Screen.width / hole.rect.width) + 3);
        i = 0;
        transitioning = true;
    }

    public void TransitionOut()
    {
        if (transitioning) return;
        startScale = hole.localScale;
        targetScale = Vector3.zero;
        i = 0;
        transitioning = true;
    }
}
