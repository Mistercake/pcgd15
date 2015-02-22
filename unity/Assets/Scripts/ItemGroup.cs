using UnityEngine;
using System.Collections;

public class ItemGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Enable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void Disable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Toggle()
    {
        bool enabled = false;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                enabled = true;
                break;
            }
        }
        if (enabled)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }
}
