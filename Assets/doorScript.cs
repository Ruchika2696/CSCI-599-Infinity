using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject NameLabel;
    public GameObject[] otherDoors;
    public static int zVelPlayer = 1;
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
