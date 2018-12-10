using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicTrigger : MonoBehaviour {

    public bool panic = false;
    [SerializeField]
    private Transform monster;

	// Use this for initialization
	void Start () {
		monster = GameObject.FindGameObjectWithTag("monster").transform;
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, monster.position) < 80f)
        {
            panic = true;
        }
        else
        {
            panic = false;
        }
	}

    private void togglePanic ()
    {
        if (panic == true)
        {
            panic = false;
        }
        else
        {
            panic = true;
        }
    }
}
