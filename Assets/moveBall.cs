using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBall : MonoBehaviour
{
    public KeyCode moveLeft;
    public KeyCode moveRight;

    public float horVel = 0;
    public int laneNum = 2;
    public string movementBlocked = "NO";
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horVel, staticVars.yVel, 5);
        if(Input.GetKeyDown(moveLeft) && (laneNum>1) && (movementBlocked == "NO"))
        {
            horVel = -5;
            StartCoroutine(stopSlide());
            laneNum -= 1;
            movementBlocked = "YES";

        }
        if (Input.GetKeyDown(moveRight) && (laneNum < 3) && (movementBlocked == "NO"))
        {
            horVel = 5;
            StartCoroutine(stopSlide());
            laneNum += 1;
            movementBlocked = "YES";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "danger")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.GetComponent<Renderer>().material.color == gameObject.GetComponent<Renderer>().material.color && (collision.gameObject.tag != "door"))
        {
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(gameObject);
            doorScript.zVelPlayer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "danger")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.name == "rampbottom")
        {
            staticVars.yVel = 2;
        }

        if (other.gameObject.name == "ramptop")
        {
            staticVars.yVel = 0;
        }

        if (other.gameObject.name == "RampcolliderL")
        {
            
            Destroy(gameObject);
        }

        if (other.gameObject.name == "RampcolliderR")
        {
            
            Destroy(gameObject);
        }
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horVel = 0;
        movementBlocked = "NO";
    }

    
}
