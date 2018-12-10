using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIMove : MonoBehaviour {

    private NPCState stateManager;
    private PeopleSpawner SpawnManager;
    private MonsterSpawner monsterManager;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform player;
    private Transform monster;
    public Behaviour halo;
    Transform city;
    MonsterSpawner getExitScript;
    // make a private field, get component, 
    //private Collider collider;

    private float moveSpeed = 3.5f;
    private float rotSpeed = 100f;
    private bool isRoaming = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    private bool roamCoroutine = false;
    private bool waitCoroutine = false;
    private bool panicStart = false;

    private List<Transform> exits;
    private int currentIndex;

    // Use this for initialization
    void Start () {
        stateManager = GetComponent<NPCState>();
        SpawnManager = transform.parent.parent.parent.GetComponent<PeopleSpawner>();
        monsterManager = transform.parent.parent.parent.GetComponent<MonsterSpawner>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monster = GameObject.FindGameObjectWithTag("monster").transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        halo = (Behaviour)gameObject.GetComponent("Halo");
        city = GameObject.FindGameObjectWithTag("city").transform;
        getExitScript = city.GetComponent<MonsterSpawner>();
        exits = monsterManager.exits;
    }

	
	// Update is called once per frame
	void Update () {
        if (stateManager.currentState == NPCState.States.roam)
        {
            if (isRoaming == false && roamCoroutine == false)
            {
                //Debug.Log("Starting Roam Coroutine");
                StartCoroutine("Roam");
            }
            if (isRotatingRight == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            }
            if (isRotatingLeft == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            }
            if (isWalking == true)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
        else if (stateManager.currentState == NPCState.States.wait && waitCoroutine == false)
        {
            StartCoroutine("Wait");
            waitCoroutine = true;
        }
        else if (stateManager.currentState == NPCState.States.panic && panicStart == false)
        {
            panicStart = true;
            Panic();
        }
        else if (stateManager.currentState == NPCState.States.follow)
        {
            navMeshAgent.destination = player.position;
            halo.enabled = false;
        }
        else if (stateManager.currentState == NPCState.States.foundExit)
        {
            Transform exit = getExitScript.exit;
            navMeshAgent.destination = exit.position;
        }
	}

    IEnumerator Roam()
    {
        int rotTime = Random.Range(1, 2);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(3, 8);

        isRoaming = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR <= 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR >= 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isRoaming = false;
        stateManager.setState();
        roamCoroutine = false;
        // halo.enabled = false;
    }

    IEnumerator Wait()
    {
        //Debug.Log("Waiting");
        yield return new WaitForSeconds(Random.Range(3,9));
        //Debug.Log("Setting new state");
        stateManager.setState();
        waitCoroutine = false;
        // halo.enabled = false;
    }

    void Panic()
    {
        //Debug.Log("Panicking");
        currentIndex = Random.Range(0,4);
        Transform exit = chooseNextExit();
        navMeshAgent.destination = exit.transform.position;
        halo.enabled = true;
    }

    private Transform chooseNextExit()
    {
        float probability = Random.Range(0.0f, 1.0f);
        if (currentIndex == 3)
        {
            if (probability <= .5f)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex--;
            }
        }
        else if (currentIndex == 0)
        {
            if (probability <= .5f)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 3;
            }
        }
        else
        {
            if (probability <= .5f)
            {
                currentIndex++;
            }
            else
            {
                currentIndex--;
            }
        }
        return exits[currentIndex].transform;

    }

    public void changeDestination()
    {
        Transform exit = chooseNextExit();
        navMeshAgent.destination = exit.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Tag : " + collision.transform.tag);
        if (collision.transform.tag == "NPC" && stateManager.currentState == NPCState.States.panic)
        {
            stateManager.currentState = NPCState.States.panic;
        }
    }

}
