using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.green;
        Material greenMat = Resources.Load("rightDoor", typeof(Material)) as Material;
        gameObject.GetComponent<Renderer>().material = greenMat;
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.acquireMagnet == true)
            gameObject.GetComponent<CapsuleCollider>().radius = 2.0f;
    }
}
