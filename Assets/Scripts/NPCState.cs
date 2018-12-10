using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState : MonoBehaviour {
    //Create enum to keep track of states
    public enum States {roam, wait, panic, follow, foundExit};

    private PanicTrigger panicScript;
    private PlayerWhistle whistleScript;
    Transform player;
    Transform city;
    Transform exit;
    private HospitalLight hospitalLightScript;
    private MonsterSpawner getExitScript;

    [SerializeField]
    public States currentState;


	// Use this for initialization
	void Start () {
        panicScript = GetComponent<PanicTrigger>();
        whistleScript = GetComponent<PlayerWhistle>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        city = GameObject.FindGameObjectWithTag("city").transform;
        getExitScript = city.GetComponent<MonsterSpawner>();
        hospitalLightScript = player.GetComponent<HospitalLight>();
        setState();
	}

    // Update is called once per frame
    void Update () {
        if (panicScript.panic == true && currentState != States.follow)
        {
            currentState = States.panic;
        }

        if (whistleScript.isFollowing == true)
        {
            currentState = States.follow;
        }

        if (hospitalLightScript.isOn == true)
        {
            Transform hospital = getExitScript.exit;
            if (Vector3.Distance(transform.position, hospital.position) < 125f)
            {
                currentState = States.foundExit;
            }
        }
    }

    public void setState()
    {
        float waitProbability = .5f;
        if (UnityEngine.Random.Range(0f, 1f) < waitProbability)
        {
            currentState = States.wait;
        }
        else
        {
            currentState = States.roam;
        }
    }
}
