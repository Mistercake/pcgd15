using UnityEngine;
using System.Collections;

public class BarsGame : MonoBehaviour, MiniGame {

    Terminal activator;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Cancel"))
        {
            activator.MiniGameQuit();
            Destroy(gameObject);
        }
	}

    public bool IsGoalReached()
    {
        return false;
    }
    public bool IsBonusGoalReached()
    {
        return false;
    }
    public void SetTerminal(Terminal terminal)
    {
        activator = terminal;
    }

    public void GoalReached()
    {
        Invoke("End", 1f);
    }

    void End()
    {
        activator.MiniGameFinished(false);
        Destroy(gameObject);
    }
}
