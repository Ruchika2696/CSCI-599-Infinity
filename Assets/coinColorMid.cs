using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinColorMid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Material yellowMat = Resources.Load("centerDoor", typeof(Material)) as Material;
        gameObject.GetComponent<Renderer>().material.color = yellowMat.color;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
