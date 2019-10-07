using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject NameLabel;
    public static int zVelPlayer = 1;
    void Start()
    {
        gameObject.tag = "door";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int num = question.num;
        string ans = question.dict[num].getAns();
        Debug.Log("ans = " + ans);
        string doorText = gameObject.GetComponentInChildren<Text>().text;
        //string doorText = gameObject.Find("Canvas/Button").GetComponentInChildren<Text>().text;
        if (other.tag == "player" && !doorText.Equals(ans))
        {
            // wrong ans
            Debug.Log("DoorText = " + doorText + " , Answer =  " + ans);
            Debug.Log("Wrong answer");
            gameObject.tag = "danger";
            zVelPlayer = 0;
            //GetComponent<Rigidbody>().velocity = 0;


        } else
        {
            other.gameObject.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
            Debug.Log("right ans");
            Destroy(gameObject);
        }
    }
    
}
