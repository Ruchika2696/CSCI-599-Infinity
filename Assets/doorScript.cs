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
    private static Dictionary<string, object> doorColorCrossCount = new Dictionary<string, object>{
            { "redDoor", 0},
            { "yellowDoor", 0},
            { "greenDoor", 0}
        };

private Material yellowMat;
    private Material redMat;
    private Material greenMat;
    private Material doorMat;

    void Start()
    {
        gameObject.tag = "door";
        yellowMat = Resources.Load("centerDoor", typeof(Material)) as Material;
        redMat = Resources.Load("leftDoor", typeof(Material)) as Material;
        greenMat = Resources.Load("rightDoor", typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            doorMat = gameObject.GetComponent<Renderer>().material;
            String key = String.Empty;
            if (doorMat.color.Equals(yellowMat.color))
            {
                key = "yellowDoor";
            }
            else if(doorMat.color.Equals(redMat.color))
            {
                key = "redDoor";
            }
            else if(doorMat.color.Equals(greenMat.color))
            {
                key = "greenDoor";
            }
            try
            {
                doorColorCrossCount[key] = Convert.ToInt32(doorColorCrossCount[key]) + 1;
            }
            catch(KeyNotFoundException)
            {
                Debug.Log("key not found in doorColorCrossCount dictionary");
            }

            Analytics.CustomEvent("doorCrossed", doorColorCrossCount);

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
