using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinColorRight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Material greenMat = Resources.Load("rightDoor", typeof(Material)) as Material;
        gameObject.GetComponent<Renderer>().material.color = greenMat.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
