using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitCollider : MonoBehaviour {
    public int saveCount = 0;
    public Text SaveCountText;
    public bool open = true;

	// Use this for initialization
	void Start () {
        SetSaveCount();
	}
	
    private void OnTriggerEnter(Collider other)
    {
        Transform otherTransform = other.transform;
        if (open)
        {
            if (otherTransform.tag == "NPC")
            {
                Destroy(other.gameObject);
                saveCount++;
                SetSaveCount();
            }
        }
        else
        {
            AIMove npcMove = otherTransform.GetComponent<AIMove>();
            if (npcMove)
            {
                npcMove.changeDestination();
            }
            
        }
    }

    void SetSaveCount()
    {
        SaveCountText.text = "Save Count: " + saveCount.ToString();
    }
}
