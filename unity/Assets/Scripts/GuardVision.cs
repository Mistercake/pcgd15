using UnityEngine;
using System.Collections;

public class GuardVision : MonoBehaviour
{

    public float GuardFOV = 120f;

    Transform eyes;
    Transform player;
    float alertBuffer = 0f;
    GuardAlertness guardAlert;
    Vector3 lastPlayerPosition;
    float lastPlayerSighting;

    // Use this for initialization
    void Start()
    {
        eyes = transform.Find("CATRigPelvis/CATRigSpine1/CATRigSpine2/CATRigTorso/CATRigSpine1 1/CATRigSpine2 1/CATRigHead/Eyes");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        guardAlert = gameObject.GetComponent<GuardAlertness>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.GetComponent<GuardMovement>().IsDead()) return;

        Vector3 playerTarget = player.position + Vector3.up * 0.5f;

        RaycastHit hit;
        if (CanSee(player, out hit, (Vector3.up * 0.5f)))
        {
            if (hit.transform.tag == "Player")
            {
                alertBuffer += Time.deltaTime;
                if (alertBuffer > 0.2f)
                {
                    VisualContact();
                }
            }
        }
        else
        {
            alertBuffer -= Time.deltaTime;
            alertBuffer = alertBuffer < 0 ? 0f : alertBuffer;
        }

        foreach (GameObject guard in GameObject.FindGameObjectsWithTag("Guard"))
        {
            if (guard == gameObject) continue;
            hit = new RaycastHit();
            if (guard.GetComponent<GuardMovement>().IsDead() && CanSee(guard.transform, out hit, Vector3.zero))
            {
                Suspicion(guard.transform.position);
                guard.gameObject.tag = "";
            }
        }

        bool playerMoving = player.GetComponent<PlayerMovement>().IsMoving();
        //Debug.Log(playerVelocity);

        if (guardAlert.GetStatus() >= GuardAlertness.STATUS_CAUTION && Vector3.Distance(playerTarget, transform.position) < 3f && playerMoving)
        {
            VisualContact();
        }

    }

    bool CanSee(Transform target, out RaycastHit hit, Vector3 offset)
    {
        hit = new RaycastHit();
        bool result = false;
        Color hitColor = Color.cyan;
        Vector3 point = target.position + offset;
        if (Vector3.Distance(point, transform.position) < 10f && Vector3.Angle(point - eyes.position, transform.forward) < GuardFOV / 2)
        {
            if (Physics.Raycast(eyes.position, (point - eyes.position), out hit))
            {
                result = true;
                hitColor = Color.red;
            }
        }
        Debug.DrawLine(eyes.position, point, hitColor);
        return result;
    }

    public Vector3 GetLastPlayerPosition()
    {
        return lastPlayerPosition;
    }

    public float GetSightingDelta()
    {
        return Time.time - lastPlayerSighting;
    }

    void VisualContact()
    {
        lastPlayerPosition = player.position;
        lastPlayerSighting = Time.time;
        guardAlert.VisualContact();
    }

    public void Suspicion(Vector3 source)
    {
        lastPlayerPosition = source;
        lastPlayerSighting = Time.time;
        guardAlert.VisualContact();
    }
}
