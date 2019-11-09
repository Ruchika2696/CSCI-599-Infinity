using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinActionsMagnet : MonoBehaviour
{
    // this script controls coin's movements towards the player
    // when the player acquires the magnet power-up

    public float magnetStrength = 5f;
    public bool looseMagnet = true;
    // magnetic force strength based on distance of coin from magnet
    public float distanceStrength = 10f;
    public float magnetTriggerDistance = 15.0f;

    private Transform trans;
    private Rigidbody thisRd;
    private Transform magnetTrans;
    public bool magnetInZone;

    private void Awake()
    {
        trans = transform;
        thisRd = trans.GetComponent<Rigidbody>();

        magnetInZone = true;
    }

    private void FixedUpdate()
    {
        // because our player object is a rigid body
        if (GM.acquireMagnet == true && magnetInZone)
        {
            // dir between the player(magnet) and coin
            if (trans != null)
            {
                magnetTrans = GameObject.FindGameObjectWithTag("player").transform;
                Vector3 directionMagnet = magnetTrans.position - trans.position;

                // distance between the player(magnet) and coin
                float distance = Vector3.Distance(magnetTrans.position, trans.position);
                if(distance <= magnetTriggerDistance)
                {
                    // magnet's strength changes based on the distance of coin to player(magnet)
                    float magnetDistanceStrength = (distanceStrength / distance) * magnetStrength;
                    Vector3 f = magnetDistanceStrength * directionMagnet;
                    if (f != new Vector3(0, 0, 0))
                    {
                        thisRd.AddForce(f, ForceMode.Force);
                    }
                }
                if (directionMagnet.z > 0)
                {
                    magnetInZone = false;
                }
            }
        }
    }
}
