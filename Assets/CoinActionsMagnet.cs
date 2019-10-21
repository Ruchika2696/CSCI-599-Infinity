using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinActionsMagnet : MonoBehaviour
{
    // this script controls coin's movements towards the player
    // when the player acquires the magnet power-up

    public float magnetStrength = 4f;
    public bool looseMagnet = false;
    // magnetic force strength based on distance of coin from magnet
    public float distanceStrength = 9f;

    private Transform trans;
    private Rigidbody thisRd;
    private Transform magnetTrans;
    private bool magnetInZone;

    private void Awake()
    {
        trans = transform;
        thisRd = trans.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // because our player object is a rigid body

        if (magnetInZone)
        {
            // dir between the player(magnet) and coin
            Vector3 directionMagnet = magnetTrans.position - trans.position;
            // distance between the player(magnet) and coin
            float distance = Vector3.Distance(magnetTrans.position, trans.position);

            // magnet's strength changes based on the distance of coin to player(magnet)
            float magnetDistanceStrength = (distanceStrength / distance) * magnetStrength;
            thisRd.AddForce(magnetDistanceStrength * directionMagnet, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("MagnetPlayer"))
        if (GM.acquireMagnet == true && other is SphereCollider)
        {
            magnetTrans = other.transform;
            magnetInZone = true;
        }
    }
}
