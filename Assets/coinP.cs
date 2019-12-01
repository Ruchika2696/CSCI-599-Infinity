using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Material yellowMat = Resources.Load("centerDoor", typeof(Material)) as Material;
        gameObject.GetComponent<Renderer>().material = yellowMat;
    }

    // Update is called once per frame
    void Update()
    {
        if(GM.acquireMagnet == true)
            gameObject.GetComponent<CapsuleCollider>().radius = 2.0f;
    }
}
