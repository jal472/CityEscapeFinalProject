using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// JACK --> SHOULD BE OK HERE
public class PlayerWhistle : MonoBehaviour {

    public bool isFollowing = false;
    [SerializeField]
    private Transform player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && Vector3.Distance(transform.position, player.position) < 25f)
        {
            toggleWhistle();
        }

    }

    private void toggleWhistle()
    {
        if (isFollowing == true)
        {
            isFollowing = false;
            Debug.Log("following is false");
        }
        else
        {
            isFollowing = true;
            Debug.Log("following is true");
        }
    }
}
