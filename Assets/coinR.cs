using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
