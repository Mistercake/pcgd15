using UnityEngine;
using System.Collections;

public class GuardMovement : MonoBehaviour
{

    public GuardPatrolNode targetNode;

    NavMeshAgent agent;
    CharacterController controller;
    Transform player;
    Animator animator;
    AlertSystem alert;
    GuardAlertness guardAlert;
    GuardVision vision;
    float lookAroundTimer = 0f;
    float defaultStoppingDistance = 1f;
    bool reachedNode = false;
    Transform punchVolume;
    AudioSource step;
    AudioSource losePlayer;

    bool search = false;
    bool dead = false;

    float stepTime = 0f;

    // Use this for initialization
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        controller = gameObject.GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = gameObject.GetComponent<Animator>();
        guardAlert = gameObject.GetComponent<GuardAlertness>();
        vision = gameObject.GetComponent<GuardVision>();
        alert = GameObject.FindGameObjectWithTag("AlertSystem").GetComponent<AlertSystem>();
        punchVolume = transform.Find("CATRigPelvis/CATRigSpine1/CATRigSpine2/CATRigTorso/CATRigRArmCollarbone/CATRigRArm1/CATRigRArm2/CATRigRArmPalm/PunchVolume");

        AudioSource[] audios = GetComponents<AudioSource>();
        step = audios[0];
        losePlayer = audios[2];
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!dead)
        {

            if (search)
            {
                Search();
            }
            else
            {
                CheckAlertStatus();
            }

            Vector3 input = Vector3.ClampMagnitude(agent.nextPosition - transform.position, 1f);
            input.y = 0f;

            if (lookAroundTimer == 0)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, input, 0.5f, 0f));
                controller.Move(agent.velocity * Time.deltaTime);
            }

            animator.SetFloat("Velocity", controller.velocity.magnitude);
            controller.Move(Vector3.down * 9.81f * Time.deltaTime);

            lookAroundTimer -= Time.deltaTime;
            lookAroundTimer = Mathf.Clamp(lookAroundTimer, 0f, 10f);
        }
    }

    void Search()
    {
        if (alert.GetSightingDelta() < 1f || alert.GetStatus() != AlertSystem.STATUS_ALERT)
        {
            search = false;
            return;
        }
        agent.speed = 1.5f;
        if (Mathf.Round(agent.remainingDistance) <= Mathf.Round(agent.stoppingDistance))
        {
            float radius = 6;
            Vector3 direction = Vector3.ProjectOnPlane(Random.insideUnitSphere, Vector3.up).normalized * radius;
            direction += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(direction, out hit, radius, 1);
            Vector3 destination = hit.position;
            agent.SetDestination(destination);
        }
    }

    void CheckAlertStatus()
    {

        switch (guardAlert.GetStatus())
        {
            case GuardAlertness.STATUS_CLEAR: // IF CLEAR
                agent.speed = 1;
                WalkPath();
                break;

            case GuardAlertness.STATUS_CAUTION: // IF CAUTION
                agent.speed = 1.5f;
                WalkPath();
                break;

            case GuardAlertness.STATUS_ALERT: // IF ALERT
                lookAroundTimer = 0f;
                if (alert.GetStatus() != AlertSystem.STATUS_ALERT && vision.GetSightingDelta() >= 10f)
                {
                    guardAlert.DropToCaution();
                    return;
                }

                agent.speed = 2.5f;
                if (alert.GetStatus() != AlertSystem.STATUS_ALERT && vision.GetSightingDelta() < 10f)
                {
                    Transform button = GetClosestAlertButton();
                    if (button != null)
                    {
                        agent.stoppingDistance = 0.5f;
                        agent.SetDestination(button.position);
                        if (Vector3.Distance(transform.position, button.position) <= 1f)
                        {
                            alert.Alert(vision.GetLastPlayerPosition());
                        }
                    }
                }
                if (alert.GetStatus() == AlertSystem.STATUS_ALERT)
                {
                    agent.stoppingDistance = defaultStoppingDistance;
                    agent.SetDestination(alert.GetLastPlayerPosition());
                    if (alert.GetSightingDelta() > 5f && Mathf.Round(agent.remainingDistance) <= Mathf.Round(agent.stoppingDistance))
                    {
                        Debug.Log("Search");
                        losePlayer.Play();
                        search = true;
                    }
                }
                if (Vector3.Distance(player.position, transform.position) <= 1.1f && Vector3.Angle(transform.forward, (player.position - transform.position)) < 30f)
                {
                    animator.SetTrigger("Punch");
                }
                break;
        }

    }

    Transform GetClosestAlertButton()
    {
        Transform result = null;
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("AlertButton"))
        {
            if (result == null || Vector3.Distance(transform.position, button.transform.position) < Vector3.Distance(transform.position, result.position))
            {
                result = button.transform;
            }
        }
        return result;
    }

    void WalkPath()
    {
        agent.SetDestination(targetNode.transform.position);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!reachedNode)
            {
                lookAroundTimer += 2f;
                targetNode = targetNode.nextNode;
                reachedNode = true;
            }
        }
        else
        {
            reachedNode = false;
        }
    }

    public void SelectTargetNode()
    {
        GuardPatrolNode result = GameObject.FindObjectOfType<GuardPatrolNode>();
        foreach (GuardPatrolNode node in GameObject.FindObjectsOfType<GuardPatrolNode>())
        {
            if (Vector3.Distance(transform.position, node.transform.position) < Vector3.Distance(transform.position, result.transform.position))
            {
                result = node;
            }
        }
        targetNode = result;
    }

    public void Die()
    {
        dead = true;
        animator.SetTrigger("Die");
        DisablePunch();
        gameObject.layer = 12; // switch to layer "Walk Over"
    }

    public bool IsDead()
    {
        return dead;
    }

    public void EnablePunch()
    {
        punchVolume.gameObject.SetActive(true);
    }

    public void DisablePunch()
    {
        punchVolume.gameObject.SetActive(false);
    }

    public void Step()
    {
        if (Time.time - stepTime > 0.1f)
        {
            step.Play();
            stepTime = Time.time;
        }
    }
}
