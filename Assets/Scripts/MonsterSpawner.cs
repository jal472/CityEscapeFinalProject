using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MonsterSpawner : MonoBehaviour {

    public List<Transform> spawnPoints = new List<Transform>();
    public List<Transform> exits = new List<Transform>();
    [SerializeField]
    public Transform exit;

    [Header("Monster")]
    [SerializeField]
    private GameObject prefab;

    private Transform monster;

    // Use this for initialization
    void Start () {
        getSpawnPoints();
        getExits();
        spawnMonster();
        disableExits();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void getSpawnPoints()
    {
        Transform[] spList = this.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < spList.Length; i++)
        {
            if (spList[i].tag == "monsterSpawn")
            {
                spawnPoints.Add(spList[i]);
            }
        }
    }

    void spawnMonster()
    {
        int randomIndex = Random.Range(0, 4);
        GameObject Monster = (GameObject)Instantiate(prefab, spawnPoints[randomIndex]);
        monster = GameObject.FindGameObjectWithTag("monster").transform;
    }

    void getExits()
    {
        Transform[] exitList = this.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < exitList.Length; i++)
        {
            if (exitList[i].tag == "exit")
            {
                exits.Add(exitList[i]);
            }
        }
    }

    void disableExits()
    {
        int i;
        float furthestDistance = 0f;
        float distanceToMonster = 0f;
        int exitIndex = 0;
        for (i = 0; i < exits.Count; i++)
        {
            distanceToMonster = Vector3.Distance(exits[i].transform.position, monster.transform.position);
            if (distanceToMonster > furthestDistance)
            {
                furthestDistance = distanceToMonster;
                exitIndex = i;
            }
        }

        exit = exits[exitIndex];

        for (i = 0; i < exits.Count; i++)
        {
            if (i != exitIndex)
            {
                exits[i].GetComponent<ExitCollider>().open = false;
                Transform hospital = exits[i].transform.parent;
                Destroy(hospital.GetChild(0).gameObject);
            }
        }
    }

    public Transform getExit()
    {
        return exit;
    }
}
