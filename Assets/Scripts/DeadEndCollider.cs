using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class DeadEndCollider : MonoBehaviour {

    //public List<Transform> escapePoints = new List<Transform>();
    BoxCollider collider;

	// Use this for initialization
	void Start () {
        collider = transform.GetComponent<BoxCollider>();
        //getEscapePoints();
	}

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        NPCState stateManager = other.GetComponent<NPCState>();
        if (other.transform.tag == "NPC")
        {
            //Debug.Log("NPC Inside Dead end");
            stateManager.currentState = NPCState.States.panic;
            //keep here just in case
            /*int random = Random.Range(0, 5);
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            agent.destination = escapePoints[random].position;*/
        }
    }

    /*private void getEscapePoints()
    {
        Transform[] escapeList = this.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < escapeList.Length; i++)
        {
            if (escapeList[i].name == "deadEndEscape")
            {
                escapePoints.Add(escapeList[i]);
            }
        }
    }*/

}
