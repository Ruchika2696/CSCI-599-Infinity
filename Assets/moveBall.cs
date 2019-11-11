using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;

public class moveBall : MonoBehaviour
{
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode space;
    public float horVel = 0;
    public int laneNum;
    public bool flag;
    int safeLane;
    public string movementBlocked;
    public Transform gameOverAnimationObject;
    SimpleTimer st;

    private bool isGrounded;
    private bool groundContact;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public DeathMenu deathScreen;
    private Material yellowMat;
    private Material redMat;
    private Material greenMat;
    float safeZ;
    float gameTime;
    GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        // get Material objects to use for coin score calculation
        yellowMat = Resources.Load("centerDoor", typeof(Material)) as Material;
        redMat = Resources.Load("leftDoor", typeof(Material)) as Material;
        greenMat = Resources.Load("rightDoor", typeof(Material)) as Material;

        gameObject.GetComponent<Renderer>().material = yellowMat;
        staticVars.redCount = 0;
        staticVars.greenCount = 0;
        staticVars.yellowCount = 0;
        staticVars.score = 0;
		staticVars.magnetCount = 0;
		staticVars.shieldCount = 0;
        safeZ = 0.78f;
        safeLane = 2;
        flag = false;
        movementBlocked = "NO";
        gameTime = 0;
        laneNum = 2;
        staticVars.paisa = false;
        groundContact = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            gameTime = 0;
            float z = gameObject.transform.position.z;
            if (safeLane == 1)
            {
                gameObject.transform.position = new Vector3(-2f, 1.14f, safeZ);

            }
            else if (safeLane == 2)
            {
                gameObject.transform.position = new Vector3(0.5f, 1.14f, safeZ);

            }

            else
            {
                gameObject.transform.position = new Vector3(3f, 1.14f, safeZ);
            }
            laneNum = safeLane;
            flag = false;
        }
        //    Debug.Log("in update"+gameObject.transform.position);
        gameTime += Time.deltaTime;
        if (gameTime > 4)
        {
            safeZ = gameObject.transform.position.z;
            safeLane = laneNum;
            gameTime = 0;
        }
        staticVars.score++;
        // Debug.Log(staticVars.score);

        //Debug.Log(staticVars.yVel);
        GetComponent<Rigidbody>().velocity = new Vector3(horVel, staticVars.yVel, 10 * doorScript.zVelPlayer);

		if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // Insert Code Here (I.E. Load Scene, Etc)
                // OR Application.Quit();
				//Instantiate(gameOverAnimationObject, transform.position, gameOverAnimationObject.rotation);
                doorScript.zVelPlayer = 0;
                staticVars.gameStatus = "GameOver";
                //Destroy(gameObject);
                return;
            }
 
	}



        if (Input.GetKeyDown(moveLeft) && (laneNum > 1) && (movementBlocked == "NO"))
        {	
			//Dictionary<string, object> data = new Dictionary<string, object>();
			//data.Add("test1", "hello");
			//Analytics.CustomEvent("test", data);
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
        /*
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > (Screen.width / 2))
            {
                //              GoRight();
                horVel = 5;
                StartCoroutine(stopSlide());
                laneNum += 1;
                movementBlocked = "YES";
            }
            if (touch.position.x < (Screen.width / 2))
            {
                //             GoLeft();
                horVel = -5;
                StartCoroutine(stopSlide());
                laneNum -= 1;
                movementBlocked = "YES";
            }
        }

		if (Input.touchCount == 2)
		{
			StartCoroutine(Jump());;
		}
		*/


        if (Input.GetKeyDown("space"))
        {

            StartCoroutine(Jump());

        }

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    groundContact = true;
                    StartCoroutine(Jump());
                }

                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {

                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    //             GoLeft();
                    if ((laneNum > 1) && (movementBlocked == "NO"))
                    {

                        horVel = -5;
                        StartCoroutine(stopSlide());
                        laneNum -= 1;
                        movementBlocked = "YES";
                    }

                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if ((laneNum < 3) && (movementBlocked == "NO"))
                    {
                        horVel = 5;
                        StartCoroutine(stopSlide());
                        laneNum += 1;
                        movementBlocked = "YES";
                    }

                }
            }
        }
    }


    int max(int a, int b)
    {
        return (a > b) ? a : b;
    }

    IEnumerator Jump()
    {

        if (groundContact)
        {
            Debug.Log("hit space");
            Vector3 up = new Vector3(0, 1.5f, 0);
            gameObject.transform.position += up;
            groundContact = false;
            yield return new WaitForSeconds(.6f);
            gameObject.transform.position -= up;
            groundContact = true;
        }

    }

    private void CoinScoreCalculator(Collider other)
    {
        if (other.gameObject.GetComponent<Renderer>().material.color == yellowMat.color)
            staticVars.yellowCount++;
        else if (other.gameObject.GetComponent<Renderer>().material.color == greenMat.color)
            staticVars.greenCount++;
        else if (other.gameObject.GetComponent<Renderer>().material.color == redMat.color)
            staticVars.redCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magnet"))
        {	
			staticVars.magnetCount+=1;
		    // when the player collides with a magnet it acquires magnet power-up
            st = new SimpleTimer("MagnetTimer", 15.0f);
            StartCoroutine(st.MagnetPowerUp());
            // if collider is a magnet
            //GM.acquireMagnet = true;

            // destroy the magnet
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Shield"))
        {	
			staticVars.shieldCount +=1;
            // when the player collides with a magnet it acquires magnet power-up
            st = new SimpleTimer("ShieldTimer", 15.0f);
            StartCoroutine(st.ShieldPowerUp());
            // if collider is a magnet
            //GM.shieldMode = true;

            // destroy the magnet
            Destroy(other.gameObject);
        }

        if (other.tag == "danger")
        {
            if (!GM.shieldMode)
            {
				
                Instantiate(gameOverAnimationObject, transform.position, gameOverAnimationObject.rotation);
                doorScript.zVelPlayer = 0;
                staticVars.gameStatus = "GameOver";
                if (staticVars.redCount >= 10 && staticVars.yellowCount >= 10 && staticVars.greenCount >= 10)
                {
                    staticVars.paisa = true;
                    deathScreen.gameObject.SetActive(true);
                }
                gameObject.SetActive(false);

                // Distance covered before dying
                Analytics.CustomEvent("distanceEvent", new Dictionary<string, object>
                        {
                            { "distanceCovered", gameObject.transform.position.z}
                        });

                //Destroy(gameObject);
                if(other.gameObject.name == "Pit")
                {
                    Analytics.CustomEvent("deathEvent", new Dictionary<string, object>
                        {
                            { "reason", "pit" },
                        });
                }
                if(other.gameObject.name == "obstacle")
                {
                     Analytics.CustomEvent("deathEvent", new Dictionary<string, object>
                      {
                            { "reason", "obstacle" },
                      });
                }
            }

        }
        else if (GM.acquireMagnet == false &&
            other.gameObject.GetComponent<Renderer>() != null &&
            other.gameObject.GetComponent<Renderer>().material.color == gameObject.GetComponent<Renderer>().material.color &&
            (other.gameObject.tag != "door"))
        {
            // Same color coins acquired
            Destroy(other.gameObject);
            CoinScoreCalculator(other);
        }
        else if (GM.shieldMode == false && GM.acquireMagnet == false &&
            GM.powerupEffect == false &&
            other.gameObject.GetComponent<Renderer>() != null &&
            other.gameObject.GetComponent<Renderer>().material.color != gameObject.GetComponent<Renderer>().material.color &&
            (other.gameObject.tag != "door"))
        {
            // Different color coins. Player dies

            //    Destroy(gameObject);
            gameObject.SetActive(false);
            if (staticVars.redCount >= 10 && staticVars.yellowCount >= 10 && staticVars.greenCount >= 10)
            {
                staticVars.paisa = true;
                deathScreen.gameObject.SetActive(true);
            }
            doorScript.zVelPlayer = 0;
            Instantiate(gameOverAnimationObject, transform.position, gameOverAnimationObject.rotation);
            staticVars.gameStatus = "GameOver";

            // Distance covered before dying
            Analytics.CustomEvent("distanceEvent", new Dictionary<string, object>
                        {
                            { "distanceCovered", gameObject.transform.position.z}
                        });

            Analytics.CustomEvent("deathEvent", new Dictionary<string, object>
              {
                      { "reason", "other coins" },
              });
            //Destroy(gameObject);
        }

        if (GM.acquireMagnet == true && other.tag == "Coin")
        {
            // when the player has magnet, on trigger, acquire coin
            // irrespective of coin and ball color
            Destroy(other.gameObject);
            CoinScoreCalculator(other);
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "danger")
    //    {
    //        Destroy(gameObject);
    //    }
    //    if (other.gameObject.name == "rampbottom")
    //    {
    //        staticVars.yVel = 2;
    //    }

    //    if (other.gameObject.name == "ramptop")
    //    {
    //        staticVars.yVel = 0;
    //    }

    //    if (other.gameObject.name == "RampcolliderL")
    //    {

    //        Destroy(gameObject);
    //    }

    //    if (other.gameObject.name == "RampcolliderR")
    //    {

    //        Destroy(gameObject);
    //    }
    //}

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horVel = 0;
        movementBlocked = "NO";
    }

    public void Reset()
    {

        if(staticVars.redCount >= 10 && staticVars.greenCount >= 10 && staticVars.yellowCount >= 10)
       //   if(true)
        {
            //     movementBlocked = "YES";
            GM.acquireMagnet = false;
            deathScreen.gameObject.SetActive(false);
            staticVars.gameStatus = "";
            staticVars.loadingTime = 0;
            staticVars.paisa = false;
            doorScript.zVelPlayer = 1;
            gameObject.SetActive(true);
            horVel = 0;
            movementBlocked = "NO";
            // Vector3 newPos = new Vector3(0, 0, 2);
            //gameObject.transform.position += newPos;
            if (safeLane == 1)
            {
                Debug.Log("SAFE Z VALUE :- " + safeZ);
                float y = gameObject.transform.position.y;
                float z = gameObject.transform.position.z;
                Vector3 newPos = new Vector3(-2f, 1.14f, safeZ);
                Debug.Log("SAFE LANE #1");
                gameObject.transform.position = newPos;
                Debug.Log(gameObject.transform.position);

            }

            if (safeLane == 2)
            {
                Debug.Log("SAFE#2 Z VALUE :- " + safeZ);
                float y = gameObject.transform.position.y;
                float z = gameObject.transform.position.z;
                float x = gameObject.transform.position.x;
                Debug.Log("SAFE LANE #2");
                Vector3 newPos = new Vector3(0.5f, 1.14f, safeZ);
                gameObject.transform.position = newPos;
                Debug.Log(gameObject.transform.position);
            }

            if (safeLane == 3)
            {
                Debug.Log("SAFE#3 Z VALUE :- " + safeZ);
                Debug.Log("SAFE LANE #3");
                float y = gameObject.transform.position.y;
                float z = gameObject.transform.position.z;
                Vector3 newPos = new Vector3(3f, 1.14f, safeZ);
                gameObject.transform.position = newPos;
                Debug.Log(gameObject.transform.position);

            }
            staticVars.redCount = max(0, staticVars.redCount - 10);
            staticVars.greenCount = max(0, staticVars.greenCount - 10);
            staticVars.yellowCount = max(0, staticVars.yellowCount - 10);
            Vector3 changeCam = new Vector3(0.8f, 3.7f, (safeZ - 8f));
            // Debug.Log(newPos + changeCam);
            GameObject.Find("Main Camera").transform.position = changeCam;
            //       movementBlocked = "NO";
            flag = true;
            //  Debug.Log("Flag#2 " + flag);
            //     StartCoroutine(stopSlide());

            //horVel = 0;
            //  st = new SimpleTimer("ResumeTimer", 1f);
            //  StartCoroutine(st.EnableMovement());
        }
        else
        {
            Debug.Log("Not enough coins");
        }

    }

}
