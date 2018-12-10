using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDistraction : MonoBehaviour {

    public float throwForce = 70f;
    public GameObject ballPrefab;
    public bool isThrown = false;
    public int ballLimit = 3;
    public int ballsThrown = 0;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1) && !isThrown && ballsThrown < ballLimit)
        {
            ThrowBall();
            ballsThrown++;
        }
	}

    void ThrowBall()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        ball.transform.parent = GameObject.FindGameObjectWithTag("ball count").transform;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        isThrown = true;
    }
}
