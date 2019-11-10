using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System;

public class doorScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject NameLabel;
    public GameObject[] otherDoors;
    public static int zVelPlayer = 1;

    private static Dictionary<string, object> doorCrossCount = new Dictionary<string, object>{
            { "noOfDoorsCrossed", 0}
        };

    void Start()
    {
        gameObject.tag = "door";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            try
            {
                doorCrossCount["noOfDoorsCrossed"] = Convert.ToInt32(doorCrossCount["noOfDoorsCrossed"]) + 1;
            }
            catch(KeyNotFoundException)
            {
                Debug.Log("key not found in doorColorCrossCount dictionary");
            }
            // Event track # doors crossed for each door color
            Analytics.CustomEvent("doorCrossed", doorCrossCount);

            other.gameObject.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
            Destroy(gameObject);
            otherDoors = GameObject.FindGameObjectsWithTag("door");
            foreach (GameObject g in otherDoors)
            {
                Destroy(g);
            }
        }
    }
}
