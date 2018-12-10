using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScenarios : MonoBehaviour {

    public Transform player;
    public GameObject canvas;
    public DataLogging dataScript;
    public Transform signCounter;
    public HospitalLight hospitalLightScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        canvas = GameObject.FindGameObjectWithTag("mainCanvas");
        dataScript = canvas.GetComponent<DataLogging>();
        signCounter = GameObject.FindGameObjectWithTag("sign count").transform;
        hospitalLightScript = player.GetComponent<HospitalLight>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "monster" || other.transform.tag == "exit")
        {
            List<string> newList = keepFirstHospital();
            postMail(newList);
            SceneManager.LoadScene(3);
            

        }
    }

    private List<string> keepFirstHospital()
    {
        bool haveHospital = false;
        List<string> newList = new List<string>();
        for (int i = 0; i < dataScript.mailData.Count; i++)
        {
            if (dataScript.mailData[i].Contains("hospital"))
            {
                if (!haveHospital)
                {
                    newList.Add(dataScript.mailData[i]);
                    haveHospital = true;
                }
            }
            else
            {
                newList.Add(dataScript.mailData[i]);
            }
        }
        return newList;
    }

    private void postMail (List<string> data)
    {
        string message = getMessage(data);

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("rutgersthrowaway2018@gmail.com");
        mail.To.Add("rstash96@gmail.com");
        mail.To.Add("jacklocasto@gmail.com");
        mail.Subject = "Game Data";
        mail.Body = message;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("rutgersthrowaway2018@gmail.com", "rutgers2018") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            return true;
        };
        smtpServer.Send(mail);
    }

    private string getMessage(List<string> data)
    {
        string tempMessage = "User actions:";
        tempMessage += System.Environment.NewLine + System.Environment.NewLine;
        foreach(string message in data)
        {
            tempMessage += message;
            tempMessage += System.Environment.NewLine;
        }

        tempMessage += System.Environment.NewLine;
        tempMessage += "Aggregated Data:";
        tempMessage += System.Environment.NewLine + System.Environment.NewLine;
        tempMessage += "Whistles: " + dataScript.whistles;
        tempMessage += System.Environment.NewLine;
        tempMessage += "Distractions thrown: " + dataScript.throws;
        tempMessage += System.Environment.NewLine;
        tempMessage += "Dead End Signs added: " + dataScript.signsAdded;
        tempMessage += System.Environment.NewLine;
        tempMessage += "Dead End Signs removed: " + dataScript.signsRemoved;
        tempMessage += System.Environment.NewLine;
        tempMessage += "Dead End Signs remaining: " + signCounter.childCount;
        tempMessage += System.Environment.NewLine;
        if (hospitalLightScript.isOn)
        {
            tempMessage += "Hospital Light Turned On: True";
        }
        else
        {
            tempMessage += "Hospital Light Turned On: false";
        }
        int saved = getSaveCount();
        int dead = getDeadCount();
        tempMessage += System.Environment.NewLine + System.Environment.NewLine;
        tempMessage += "People Data:";
        tempMessage += System.Environment.NewLine + System.Environment.NewLine;
        tempMessage += "Saved: " + saved;
        tempMessage += System.Environment.NewLine;
        tempMessage += "Dead: " + dead;
        tempMessage += System.Environment.NewLine;
        tempMessage += "Unaccounted For: " + (80 - (dead + saved));

        return tempMessage;
    }

    private int getDeadCount()
    {
        Transform monster = GameObject.FindGameObjectWithTag("monster").transform;
        NPCDeath deadCountScript = monster.GetComponent<NPCDeath>();
        return deadCountScript.DeadCount;
    }

    private int getSaveCount()
    {
        Transform mainExit;
        ExitCollider script = new ExitCollider();
        GameObject[] exitList = GameObject.FindGameObjectsWithTag("exit");
        foreach (GameObject exit in exitList)
        {
            script = exit.GetComponent<ExitCollider>();
            if (script.open)
            {
                mainExit = exit.transform;
                break;
            }
        }

        return script.saveCount;
    }
}
