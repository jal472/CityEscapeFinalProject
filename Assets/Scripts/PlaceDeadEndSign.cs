using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDeadEndSign : MonoBehaviour {

    public GameObject deadEndSign;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("holding space");
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Clicking");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                if(Physics.Raycast(ray, out hit))
                {
                    GameObject tile = hit.collider.gameObject;
                    float offset = 2.3f;
                    if (signPresent(tile))
                    {
                        removeSign(tile, hit);
                    }
                    else
                    {
                        if (tile.tag == "signBlock1")
                        {
                            GameObject sign = Instantiate(deadEndSign, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z), Quaternion.Euler(0, -90, -90));
                            sign.transform.parent = GameObject.FindGameObjectWithTag("sign count").transform;
                            GameObject temp = new GameObject("Temp");
                            Instantiate(temp, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z), Quaternion.Euler(0, -90, -90));
                            temp.transform.parent = tile.transform;
                        }
                        else if (tile.tag == "signBlock2")
                        {
                            GameObject sign = Instantiate(deadEndSign, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z), Quaternion.Euler(0, 90, -90));
                            sign.transform.parent = GameObject.FindGameObjectWithTag("sign count").transform;
                            GameObject temp = new GameObject("Temp");
                            Instantiate(temp, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z), Quaternion.Euler(0, 90, -90));
                            temp.transform.parent = tile.transform;
                        }
                        else if (tile.tag == "signBlock3")
                        {
                            GameObject sign = Instantiate(deadEndSign, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z), Quaternion.Euler(0, 0, -90));
                            sign.transform.parent = GameObject.FindGameObjectWithTag("sign count").transform;
                            GameObject temp = new GameObject("Temp");
                            Instantiate(temp, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z), Quaternion.Euler(0, 0, -90));
                            temp.transform.parent = tile.transform;
                        }
                        else if (tile.tag == "signBlock4")
                        {
                            GameObject sign = Instantiate(deadEndSign, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z), Quaternion.Euler(0, -180, -90));
                            sign.transform.parent = GameObject.FindGameObjectWithTag("sign count").transform;
                            GameObject temp = new GameObject("Temp");
                            Instantiate(temp, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z), Quaternion.Euler(0, -180, -90));
                            temp.transform.parent = tile.transform;
                        }
                    }
                }
            }
        }
	}

    private bool signPresent(GameObject tile)
    {
        bool present = false;
        Transform[] children = tile.GetComponentsInChildren<Transform>();
        if (children == null)
        {
            return false;
        }
        foreach (Transform child in children)
        {
            if (child.name == "Temp")
            {
                present = true;
            }
        }

        return present;
    }

    private void removeSign(GameObject tile, RaycastHit hit)
    {
        Vector3 position = new Vector3();
        Quaternion rotation = new Quaternion();
        float offset = 2.3f;
        if (tile.tag == "signBlock1")
        {
            position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z);
            rotation = Quaternion.Euler(0, -90, -90);
        }
        else if (tile.tag == "signBlock2")
        {
            position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z);
            rotation = Quaternion.Euler(0, 90, -90);
        }
        else if (tile.tag == "signBlock3")
        {
            position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z);
            rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (tile.tag == "signBlock4")
        {
            position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z);
            rotation = Quaternion.Euler(0, -180, -90);
        }

        Transform[] children = tile.GetComponentsInChildren<Transform>();
        if (children == null)
        {
            return;
        }
        foreach (Transform child in children)
        {
            if (child.name == "Temp")
            {
                Destroy(child.gameObject);
            }
        }

        Transform signParent = GameObject.FindGameObjectWithTag("sign count").transform;
        Transform[] signList = signParent.GetComponentsInChildren<Transform>();
        if (signList == null)
        {
            return;
        }
        foreach (Transform sign in signList)
        {
            if (sign.tag == "deadEnd")
            {
                if (System.Math.Round(sign.position.x, 2) == System.Math.Round(position.x, 2) && System.Math.Round(sign.position.y, 2) == System.Math.Round(position.y, 2) && System.Math.Round(sign.position.z, 2) == System.Math.Round(position.z, 2)
                    && System.Math.Round(sign.rotation.x, 2) == System.Math.Round(rotation.x, 2) && System.Math.Round(sign.rotation.y, 2) == System.Math.Round(rotation.y, 2) && System.Math.Round(sign.rotation.z, 2) == System.Math.Round(rotation.z, 2))
                {
                    Destroy(sign.gameObject);
                }
            }
        }
    }
}
