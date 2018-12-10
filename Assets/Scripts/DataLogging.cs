using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLogging : MonoBehaviour {

    public HospitalLight hospitalLightScript;
    public ThrowDistraction throwDistractionScript;
    public List<string> mailData;
    public Text timerText;
    private float startTime;
    public Transform player;
    public Transform signCounter;
    public Transform ballCounter;
    public bool throwMethodStart = false;

    public int whistles = 0;
    public int throws = 0;
    public int lastSignCount = 0;
    public int signsAdded = 0;
    public int signsRemoved = 0;
    public int lastBallCount = 0;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        signCounter = GameObject.FindGameObjectWithTag("sign count").transform;
        ballCounter = GameObject.FindGameObjectWithTag("ball count").transform;
        startTime = Time.time;
        mailData = new List<string>();
        throwDistractionScript = Camera.main.GetComponent<ThrowDistraction>();
        hospitalLightScript = player.GetComponent<HospitalLight>();
    }

    // Update is called once per frame
    void Update()
    {
        // timer
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        if (Input.GetKeyDown(KeyCode.F))
        {
            mailData.Add("Player has whistled at time: " + t);
            whistles++;
        }

        Debug.Log(signCounter.childCount);
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("holding space");
            if (Input.GetMouseButtonUp(0))
            {
                if (lastSignCount < signCounter.childCount)
                {
                    mailData.Add("Player has placed dead end sign at time: " + t);
                    lastSignCount = signCounter.childCount;
                    signsAdded++;
                }
                else if (lastSignCount > signCounter.childCount)
                {
                    mailData.Add("Player has removed dead end sign at time: " + t);
                    lastSignCount = signCounter.childCount;
                    signsRemoved++;
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (lastBallCount < ballCounter.childCount)
            {
                mailData.Add("Player has thrown distraction at time: " + t);
                lastBallCount = ballCounter.childCount;
                throws++;
            }
        }
        if (lastBallCount > ballCounter.childCount)
        {
            Debug.Log("Monster has popped the ball, reset the lastBallCount to zero");
            lastBallCount = 0;
        }

        if (hospitalLightScript.isOn && Input.GetMouseButtonUp(0))
        {
            bool stop = false;
            if (!stop)
            {
                mailData.Add("Player has turned on hospital light at time: " + t);
                stop = true;
            }
        }
        
    }
}
