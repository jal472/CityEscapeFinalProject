using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalLight : MonoBehaviour {

    public bool isOn = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "cross")
                {
                    Transform cross = hit.collider.gameObject.transform.parent;
                    List<Transform> crossComponents = getChildren(cross);
                    foreach (Transform comp in crossComponents)
                    {
                        GameObject crossComponent = comp.gameObject;
                        Behaviour halo = (Behaviour)crossComponent.GetComponent("Halo");
                        halo.enabled = true;
                        isOn = true;
                    }
                }
                
            }
        }
    }

    private List<Transform> getChildren(Transform crossTrans)
    {
        List<Transform> listOfChildren = new List<Transform>();
        for (int i = 0; i < crossTrans.childCount; i++)
        {
            listOfChildren.Add(crossTrans.GetChild(i));
        }
        return listOfChildren;
    }
}
