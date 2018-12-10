using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDeath : MonoBehaviour {

    public static NPCDeath control;
    public int DeadCount = 0;
    private Text DeathCountText;
    GameObject camera;
    ThrowDistraction ballScript;

    // Use this for initialization
    void Start ()
    {
        GetDeathText();
        SetDeadCount();
    }

    private void GetDeathText()
    {
        GameObject text = GameObject.Find("DeathCountText");
        if (text != null)
        {
            DeathCountText = text.GetComponent<Text>();
        }
    }

    //add attack animation when this occurs
    private void OnTriggerEnter(Collider other)
    {
        if (DeadCount == -1)
        {
            DeadCount = 0;
        }
        if (other.transform.tag == "NPC")
        {
            // there is where we can spawn the blood pool when the player dies...
            Destroy(other.gameObject);
            DeadCount++;
            SetDeadCount();
        }

        if (other.transform.tag == "distraction")
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
            ballScript = camera.GetComponent<ThrowDistraction>();
            Debug.Log(ballScript);
            // there is where we can spawn the blood pool when the player dies...
            if (ballScript.isThrown)
            {
                Destroy(other.gameObject);
                ballScript.isThrown = false;
            }
            
        }
    }

    void SetDeadCount()
    {
        DeathCountText.text = "Death Count: " + DeadCount;
    }
}
