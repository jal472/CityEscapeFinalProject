using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIPeople
{
    public string AIGroupName { get { return m_aiGroupName; } }
    public GameObject objectPrefab { get { return m_prefab; } }
    
    [SerializeField]
    private string m_aiGroupName;
    [SerializeField]
    private GameObject m_prefab;

    public AIPeople(string name, GameObject prefab)
    {
        this.m_aiGroupName = name;
        this.m_prefab = prefab;
    }
}

public class PeopleSpawner : MonoBehaviour {

    public List<Transform> waypoints = new List<Transform>();
    public List<Transform> exits = new List<Transform>();

    [Header("AI Groups")]
    public AIPeople[] AIObject = new AIPeople[5];

    // Use this for initialization
    void Start()
    {
        GetWaypoints();
        SpawnNPCs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnNPCs()
    {
        //for testing, use one agent
        //GameObject NPC = (GameObject)Instantiate(AIObject[0].objectPrefab, waypoints[0]);
        int j = 0;
        for (int i =0; i < waypoints.Count; i++)
        {
            if (j == 5)
            {
                j = 0;
            }
            //Debug.Log("i : "+ i + " j : "+j);
            while(j < AIObject.Length)
            {
                //spawn next
                GameObject NPC = (GameObject)Instantiate(AIObject[j].objectPrefab, waypoints[i]);
                j++;
                break;
            }
            
        }
    }

    public Vector3 RandomWaypoint()
    {
        int randomWP = Random.Range(0, (waypoints.Count - 1));
        Vector3 randomWaypoint = waypoints[randomWP].transform.position;
        return randomWaypoint;
    }

    void GetWaypoints()
    {
        Transform[] wpList = this.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < wpList.Length; i++)
        {
            if (wpList[i].tag == "waypoint")
            {
                waypoints.Add(wpList[i]);
            }
        }
    }

}
