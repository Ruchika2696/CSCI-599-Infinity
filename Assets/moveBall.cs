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
    int preSafeLane;
    public string movementBlocked;
    public Transform gameOverAnimationObject;
    public GameObject magnetPowerUpObject;
    public GameObject shieldPowerUpObject;
    SimpleTimer st;

    GameObject resumeCoins;
    private bool isGrounded;
    private bool groundContact;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public float safeZVelPlayer;
    public DeathMenu deathScreen;
    public PauseMenu pauseScreen;
    private Material yellowMat;
    private Material redMat;
    public CoinsNeeded coinsNeeded;
    int currRed;
    private bool jumpFlag;
    int currYellow;
    int currGreen;
    private Material greenMat;
    float safeZ;
    float preSafeZ;
    float camTime;
    float gameTime;
    float gameTime1;
    private bool shownOnce;
    GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        // get Material objects to use for coin score calculation
        yellowMat = Resources.Load("centerDoor", typeof(Material)) as Material;
        redMat = Resources.Load("leftDoor", typeof(Material)) as Material;
        greenMat = Resources.Load("rightDoor", typeof(Material)) as Material;

        //gameObject.GetComponent<Renderer>().material = yellowMat;
        if (deathScreen != null && deathScreen.gameObject != null)
        {
            if (deathScreen.gameObject.activeSelf == true)
            {
                resumeCoins = GameObject.Find("ResumeCoins");
            }
        }
        staticVars.redCount = 0;
        staticVars.greenCount = 0;
        staticVars.yellowCount = 0;
        currRed = 10;
        currYellow = 10;
        currGreen = 10;
        staticVars.score = 0;
        jumpFlag = true;
		staticVars.magnetCount = 0;
		staticVars.shieldCount = 0;
        safeZ = 0.78f;
        safeZVelPlayer = 1f;
        safeLane = 2;
        preSafeZ = 0.78f;
        preSafeLane = 2;
        flag = false;
        movementBlocked = "NO";
        gameTime = 0;
        gameTime1 = 0;
        laneNum = 2;
        staticVars.paisa = false;
        groundContact = true;
        shownOnce = false;
        camTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        camTime += Time.deltaTime;
        if (camTime > 5)
        {
            float camera_position_x = GameObject.Find("Main Camera").transform.position.x;
            float camera_position_y = GameObject.Find("Main Camera").transform.position.y;
            float camera_position_z = GameObject.Find("Main Camera").transform.position.z;

            float player_position_z = gameObject.transform.position.z;

            if ((Mathf.Abs(player_position_z - camera_position_z) < 6f)
                || (Mathf.Abs(player_position_z - camera_position_z) > 8f))
            {
                Vector3 changeCam = new Vector3(camera_position_x, camera_position_y, (player_position_z - 8f));
                GameObject.Find("Main Camera").transform.position = changeCam;
            }
            camTime = 0;
        }
        if (shownOnce == false)
        {
            if(currRed > staticVars.redCount && (currRed - staticVars.redCount <= 4) && staticVars.greenCount >= (currGreen - 4) && staticVars.yellowCount >= (currYellow - 4))
            {
                coinsNeeded.gameObject.SetActive(true);
                StartCoroutine(coinsFlag());
            }

            else if (currYellow > staticVars.yellowCount && (currYellow - staticVars.yellowCount <= 4) && staticVars.greenCount >= (currGreen - 4) && staticVars.redCount >= (currRed - 4))
            {
                coinsNeeded.gameObject.SetActive(true);
                StartCoroutine(coinsFlag());
            }

            else if (currGreen > staticVars.greenCount && (currGreen - staticVars.greenCount <= 4) && staticVars.redCount >= (currRed - 4) && staticVars.yellowCount >= (currYellow - 4))
            {
                coinsNeeded.gameObject.SetActive(true);
                StartCoroutine(coinsFlag());
            }
        }

        if (flag)
        {
        //    Debug.Log("UPDATE SPHERE :- " + gameObject.activeSelf);
            gameTime = 0;
            float z = gameObject.transform.position.z;

            if (Mathf.Abs(z - safeZ) < 9f)
            {
                safeLane = preSafeLane;
                safeZ = preSafeZ;
            }

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
            Vector3 changeCam = new Vector3(0.8f, 3.7f, (safeZ - 8f));
            GameObject.Find("Main Camera").transform.position = changeCam;
            flag = false;

        }
        //    Debug.Log("in update"+gameObject.transform.position);
        gameTime += Time.deltaTime;
        gameTime1 += Time.deltaTime;
        if(gameTime1 > 1 && groundContact == true)
        {
            gameTime1 = 0;
            if (movementBlocked == "NO")
            {
                if (laneNum == 1)
                {
                    gameObject.transform.position = new Vector3(-2f, 1.14f, gameObject.transform.position.z);
                }

                else if (laneNum == 2)
                {
                    gameObject.transform.position = new Vector3(0.5f, 1.14f, gameObject.transform.position.z);
                }

                else
                {
                    gameObject.transform.position = new Vector3(3f, 1.14f, gameObject.transform.position.z);
                }
            }
        }
        if (gameTime > 2)
        {
            preSafeZ = safeZ;
            preSafeLane = safeLane;
            safeZ = gameObject.transform.position.z;
            safeLane = laneNum;
            gameTime = 0;
        }
        if(pauseScreen.gameObject.activeSelf != true)
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
                if (doorScript.zVelPlayer > 0)
                    safeZVelPlayer = doorScript.zVelPlayer;
                doorScript.zVelPlayer = 0.0f;
                pauseScreen.gameObject.SetActive(true);
                jumpFlag = false;
                staticVars.gameStatus = "Paused"; 
                movementBlocked = "YES";
            }
 
	    }

        if (Input.GetKeyDown(moveLeft) && (laneNum > 1) && (movementBlocked == "NO"))
        {	
			//Dictionary<string, object> data = new Dictionary<string, object>();
			//data.Add("test1", "hello");
			//Analytics.CustomEvent("test", data);
            horVel = -25;
            StartCoroutine(stopSlide());
            laneNum -= 1;
            movementBlocked = "YES";

        }
        if (Input.GetKeyDown(moveRight) && (laneNum < 3) && (movementBlocked == "NO"))
        {
            horVel = 25;
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
         //   Debug.Log("JUMP FLAG ? :- " + jumpFlag);
         //   Debug.Log("GROUND CONTACT :- " + groundContact);
            if (groundContact && jumpFlag)
                StartCoroutine(Jump());

        }

        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 &&  currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                if (groundContact && jumpFlag)
                    StartCoroutine(Jump());
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                //Swipe down
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f &&  currentSwipe.y < 0.5f)
            {
                //             GoLeft();
                if ((laneNum > 1) && (movementBlocked == "NO"))
                {

                    horVel = -25;
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
                    horVel = 25;
                    StartCoroutine(stopSlide());
                    laneNum += 1;
                    movementBlocked = "YES";
                }
            }
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
                    //groundContact = true;
                    if (groundContact && jumpFlag)
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

                        horVel = -25;
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
                        horVel = 25;
                        StartCoroutine(stopSlide());
                        laneNum += 1;
                        movementBlocked = "YES";
                    }

                }
            }
        }
    }

    IEnumerator coinsFlag()
    {
        shownOnce = true;
        yield return new WaitForSeconds(2f);
        Debug.Log("Before Coins Needed :- " + coinsNeeded.gameObject.activeSelf);
        coinsNeeded.gameObject.SetActive(false);
        Debug.Log("After Coins Needed :- " + coinsNeeded.gameObject.activeSelf);
    }

    int max(int a, int b)
    {
        return (a > b) ? a : b;
    }

    IEnumerator Jump()
    {
        if (groundContact && jumpFlag)
        {
            groundContact = false;
            float x = gameObject.transform.position.x;
            float z = gameObject.transform.position.z;

            Vector3 up = new Vector3(x, 3.75f, z);
            gameObject.transform.position = up;
            yield return new WaitForSeconds(.6f);
            x = gameObject.transform.position.x;
            z = gameObject.transform.position.z;
            up = new Vector3(x, 1.14f, z + 0.2f);
            gameObject.transform.position = up;
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
            GameObject clone = (GameObject)Instantiate(magnetPowerUpObject, transform.position, Quaternion.identity);
            clone.transform.SetParent(gameObject.transform);
            Object.Destroy(clone, 15.0f);
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
            GameObject clone =  (GameObject) Instantiate(shieldPowerUpObject, transform.position, Quaternion.identity);
            clone.transform.SetParent(gameObject.transform);
            Object.Destroy(clone, 16.0f);
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
                if (doorScript.zVelPlayer > 0)
                    safeZVelPlayer = doorScript.zVelPlayer;
                doorScript.zVelPlayer = 0.0f;
                staticVars.gameStatus = "GameOver";
                if (staticVars.redCount >= currRed && staticVars.yellowCount >= currYellow && staticVars.greenCount >= currGreen)
                {
                    staticVars.paisa = true;
                    deathScreen.gameObject.SetActive(true);
                    resumeCoins = GameObject.Find("ResumeCoins");
                    resumeCoins.GetComponent<TextMeshProUGUI>().text = "YOU DIED! USE " + currGreen.ToString() + " COINS EACH TO REVIVE ?";
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
            if (staticVars.redCount >= currRed && staticVars.yellowCount >= currYellow && staticVars.greenCount >= currGreen)
            {
                staticVars.paisa = true;
                deathScreen.gameObject.SetActive(true);
                resumeCoins = GameObject.Find("ResumeCoins");
                resumeCoins.GetComponent<TextMeshProUGUI>().text = "YOU DIED! USE " + currGreen.ToString() + " COINS EACH TO REVIVE ?";
            }
            if (doorScript.zVelPlayer > 0)
                safeZVelPlayer = doorScript.zVelPlayer;
            doorScript.zVelPlayer = 0.0f;
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
        yield return new WaitForSeconds(.1f);
        horVel = 0;
        movementBlocked = "NO";
    }

    public void Reset()
    {

        if(staticVars.redCount >= currRed && staticVars.greenCount >= currGreen && staticVars.yellowCount >= currYellow)
       //   if(true)
        {
            jumpFlag = true;
            shownOnce = false;
            groundContact = true;
            //     movementBlocked = "YES";
            GM.acquireMagnet = false;
            deathScreen.gameObject.SetActive(false);
            staticVars.gameStatus = "";
            staticVars.loadingTime = 0;
            staticVars.paisa = false;
            doorScript.zVelPlayer = safeZVelPlayer;
            gameObject.SetActive(true);
          //  Debug.Log("RESET SPHERE :- " + gameObject.activeSelf);
            horVel = 0;
            movementBlocked = "NO";
            // Vector3 newPos = new Vector3(0, 0, 2);
            //gameObject.transform.position += newPos;

            if(Mathf.Abs(gameObject.transform.position.z - safeZ) < 2f)
            {
                safeLane = preSafeLane;
                safeZ = preSafeZ;
            }

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

            staticVars.redCount = max(0, staticVars.redCount - currRed);
            staticVars.greenCount = max(0, staticVars.greenCount - currGreen);
            staticVars.yellowCount = max(0, staticVars.yellowCount - currYellow);
            currRed += 2;
            currYellow += 2;
            currGreen += 2;
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


    public void Resume()
    {
        pauseScreen.gameObject.SetActive(false);
        Debug.Log("SAFE Z VELOCITY :- " + safeZVelPlayer);
        doorScript.zVelPlayer = safeZVelPlayer;
        movementBlocked = "NO";
        groundContact = true;
        jumpFlag = true;
    }

    public void Quit()
    {
        staticVars.quitCount++;
        Analytics.CustomEvent("PlayGame", new Dictionary<string, object>
        {
            { "QuitCount", staticVars.quitCount }
        });
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
