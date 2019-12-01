using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinColorLeft : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Material redMat = Resources.Load("leftDoor", typeof(Material)) as Material;
        gameObject.GetComponent<Renderer>().material.color = redMat.color;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
