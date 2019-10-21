using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform bbNoObst;

    // set this variable to true on trigger/collision
    // with magnet gameObject
    public static bool acquireMagnet;

    void Start()
    {
        //Instantiate(bbNoObst, new Vector3(0.5f,0f,27.07f), bbNoObst.rotation);
        //Instantiate(bbNoObst, new Vector3(0.5f, 0f, 33.07f), bbNoObst.rotation);
        //Instantiate(bbNoObst, new Vector3(0.5f, 0f, 37.07f), bbNoObst.rotation);
        //Instantiate(bbNoObst, new Vector3(0.5f, 0f, 43.07f), bbNoObst.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
