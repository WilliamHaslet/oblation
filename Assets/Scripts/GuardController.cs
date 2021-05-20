using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour {

    public Transform[] points;
    private int destPoint = 0;
    public NavMeshAgent agent;

    private GameObject target;
    public Animator animator;
    private bool hasSeenPlayer;
    public float giveUpTime;
    private float giveUpTimeStore;
    public int index;
    public float moveSpeed;

    void Start()
    {

        agent.speed = moveSpeed;

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        giveUpTimeStore = giveUpTime;

        GotoNextPoint();

    }

    public void GotoNextPoint()
    {

        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;

    }

    private void GoToTarget()
    {

        agent.destination = target.transform.position;
        //agent.destination = PlayerController.sharedPlayerControllerInstance.transform.position;

    }

    void Update()
    {
        if (target != null)
        {
            if (moveSpeed != 8)
            {
                moveSpeed = 8;
                agent.speed = 8;
                animator.SetBool("running", true);
            }
        }
        else
        {
            if (moveSpeed != 3)
            {
                moveSpeed = 3;
                agent.speed = 3;
                animator.SetBool("running", false);
            }  
        }

        if (target != null && giveUpTime > 0)
        {

            if (Mathf.Sqrt(Mathf.Pow(transform.position.z - target.transform.position.z, 2) + Mathf.Pow(transform.position.x - target.transform.position.x, 2)) < 2)
            {
                //PlyaerKillingAnimation
                if (target.CompareTag("Villager"))
                {
                    Destroy(target);
                }
                else if (target.CompareTag("Player"))
                {
                    GameManager.sharedGameManagerInstance.KillPlayer();
                    GameManager.sharedGameManagerInstance.deathText.text = "You were killed by the guards.";
                }
            }

            giveUpTime -= Time.deltaTime;

            GoToTarget();

        }
        else if (target != null && giveUpTime <= 0)
        {

            target = null;

            giveUpTime = giveUpTimeStore;

        }
        else
        {

            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();

        }

    }

    private IEnumerator SlowSpeed(int seconds)
    {
        agent.speed = moveSpeed / 3;
        yield return new WaitForSeconds(seconds);
        agent.speed = moveSpeed;
    }
    public void Handicap()
    {

        StartCoroutine(SlowSpeed(Random.Range(15, 30)));

    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            target = other.gameObject;

        }
        else if (other.CompareTag("Villager"))
        {
            if (other.GetComponent<VillagerController>().influenced)
            {
                target = other.gameObject;
            }
        }

    }

}
