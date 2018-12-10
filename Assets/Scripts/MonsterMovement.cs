using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterMovement : MonoBehaviour
{
    public GameObject mainTarget;
    public GameObject monster;
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool running = false;
    private bool idle = false;
    private GameObject target;
    private List<GameObject> allTargets;
    public int maxDistance = 1;
    private float shortestDistance;
    public int rotationSpeed = 10;
    public float moveSpeed = 5f;
    private bool hasTarget = false;
    private float distance;
    private float storeddistance = 100;
    private bool hasBall = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monster = GameObject.FindGameObjectWithTag("monster");
    }

    void Update()
    {
        GameObject[] npcTargets = GameObject.FindGameObjectsWithTag("NPC");
        allTargets = new List<GameObject>(npcTargets);
        allTargets.Add(player);
        

        shortestDistance = 1000f;
        foreach (GameObject target in allTargets)
        {
            float targetDistance = Vector3.Distance(target.transform.position, transform.position);
            if (targetDistance < shortestDistance)
            {
                shortestDistance = targetDistance;
                mainTarget = target;
            }
        }
        hasTarget = true;

        GameObject ball = GameObject.FindGameObjectWithTag("distraction");
        if (ball != null)
        {
            allTargets.Add(ball);
            mainTarget = ball;
            hasBall = true;
        }
        hasTarget = true;

        if (ArrayContains(allTargets, mainTarget))
        {
            if (mainTarget != null)
            {
                if (hasBall)
                {
                    navMeshAgent.destination = mainTarget.transform.position;
                    running = true;
                }
                else
                {
                    if (Vector3.Distance(mainTarget.transform.position, monster.transform.position) > maxDistance)
                    {
                        navMeshAgent.destination = mainTarget.transform.position;
                        running = true;
                    }
                }
            }

           animator.SetBool("running", running);
        }

        if (allTargets.Count == 0)
        {
            animator.SetBool("idle", idle);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "NPC")
        {
            hasTarget = false;
        }

        if (other.transform.tag == "distraction")
        {
            hasTarget = false;
            hasBall = false;
        }
    }



    bool ArrayContains (List<GameObject> array, GameObject g)
    {
        for (int i = 0; i< array.Count; i++)
        {
            if (array[i] == g)
            {
                return true;
            }
        }
        return false;
    }

}