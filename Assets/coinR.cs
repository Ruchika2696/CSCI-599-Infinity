using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        Material redMat = Resources.Load("leftDoor", typeof(Material)) as Material;
        gameObject.GetComponent<Renderer>().material = redMat;
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.acquireMagnet == true)
            gameObject.GetComponent<CapsuleCollider>().radius = 2.0f;
    }
}
