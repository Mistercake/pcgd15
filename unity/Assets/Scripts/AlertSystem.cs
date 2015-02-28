using UnityEngine;
using System.Collections;

public class AlertSystem : MonoBehaviour
{

    public Light mainLight;

    public const int STATUS_CLEAR = 0;
    public const int STATUS_CAUTION = 1;
    public const int STATUS_ALERT = 2;

    int currentStatus = STATUS_CLEAR;
    Color targetColor = Color.red;
    Vector3 lastPlayerPosition;
    float lastPlayerSighting;
    Audio_fade_manager music;

    // Use this for initialization
    void Start()
    {
        music = GameObject.FindObjectOfType<Audio_fade_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStatus)
        {
            case STATUS_CLEAR:
                music.alarmMusic = false;
                break;
            case STATUS_CAUTION:
                DefaultLight();
                music.alarmMusic = false;
                break;
            case STATUS_ALERT:
                music.alarmMusic = true;
                AlertLight();
                if (GetSightingDelta() > 30f)
                {
                    currentStatus = STATUS_CAUTION;
                }
                break;
        }
    }

    void DefaultLight()
    {
        mainLight.color = Color.white;
    }

    void AlertLight()
    {
        if (mainLight.color != targetColor)
        {
            mainLight.color = Color.Lerp(mainLight.color, targetColor, 0.3f);
        }
        else
        {
            if (targetColor == Color.red)
            {
                targetColor = Color.white;
            }
            else
            {
                targetColor = Color.red;
            }
        }
    }

    public void Alert(Vector3 playerPosition)
    {
        currentStatus = STATUS_ALERT;
        lastPlayerPosition = playerPosition;
        lastPlayerSighting = Time.time;
        foreach (GuardAlertness guard in GameObject.FindObjectsOfType<GuardAlertness>())
        {
            guard.Alert();
        }
    }

    public int GetStatus()
    {
        return currentStatus;
    }

    public Vector3 GetLastPlayerPosition()
    {
        return lastPlayerPosition;
    }

    public float GetSightingDelta()
    {
        return Time.time - lastPlayerSighting;
    }
}
