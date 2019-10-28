using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBall : MonoBehaviour
{
    public KeyCode moveLeft;
    public KeyCode moveRight;
	public KeyCode space;
    public float horVel = 0;
    public int laneNum = 2;
    public string movementBlocked = "NO";
    public Transform gameOverAnimationObject;
    SimpleTimer st;
    
	private bool isGrounded;
    private bool groundContact;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
    Vector2 currentSwipe;

    private Material yellowMat;
    private Material redMat;
    private Material greenMat;

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
		groundContact = true;
    }

    // Update is called once per frame
    void Update()
    {
        staticVars.score++;
       // Debug.Log(staticVars.score);

        //Debug.Log(staticVars.yVel);
        GetComponent<Rigidbody>().velocity = new Vector3(horVel, staticVars.yVel, 10 * doorScript.zVelPlayer);
		
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

		if (Input.touches.Length>0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
         {
              //save began touch 2d point
             firstPressPos = new Vector2(t.position.x,t.position.y);
         }
		 if(t.phase == TouchPhase.Ended)
         {
              //save ended touch 2d point
             secondPressPos = new Vector2(t.position.x,t.position.y);
                           
              //create vector from the two points
             currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
               
             //normalize the 2d vector
             currentSwipe.Normalize();

			  //swipe upwards
             if(currentSwipe.y > 0  && currentSwipe.x > -0.5f  && currentSwipe.x < 0.5f)
             {
                 groundContact = true;
				 StartCoroutine(Jump());
             }

			 //swipe down
             if(currentSwipe.y < 0  && currentSwipe.x >  -0.5f && currentSwipe.x < 0.5f)
             {
                 
             }
             //swipe left
             if(currentSwipe.x < 0  && currentSwipe.y > -0.5f  && currentSwipe.y < 0.5f)
             {
                 //             GoLeft();
				 if( (laneNum>1) && (movementBlocked == "NO")){
				 
					horVel = -5;
					StartCoroutine(stopSlide());
					laneNum -= 1;
					movementBlocked = "YES";
				 }
                
             }
             //swipe right
             if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
             {	
				if((laneNum < 3) && (movementBlocked == "NO")){
					horVel = 5;
					StartCoroutine(stopSlide());
					laneNum += 1;
					movementBlocked = "YES";
				}
                 
             }
		}
		}
    }

	IEnumerator Jump()
	{

			if(groundContact){
				Debug.Log("hit space");
				Vector3 up = new Vector3(0,1.5f,0);
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
            // when the player collides with a magnet it acquires magnet power-up
            Debug.Log("triggered on magnet contact");

            st = new SimpleTimer("MagnetTimer", 15.0f);
            StartCoroutine(st.MagnetPowerUp());
            // if collider is a magnet
            GM.acquireMagnet = true;

            // destroy the magnet
            Destroy(other.gameObject);
        }

        if (other.tag == "danger")
        {
            Instantiate(gameOverAnimationObject, transform.position, gameOverAnimationObject.rotation);
            doorScript.zVelPlayer = 0;
            staticVars.gameStatus = "GameOver";
            Destroy(gameObject);
        }

        else if (GM.acquireMagnet == false &&
            other.gameObject.GetComponent<Renderer>() != null &&
            other.gameObject.GetComponent<Renderer>().material.color == gameObject.GetComponent<Renderer>().material.color &&
            (other.gameObject.tag != "door"))
        {
            Destroy(other.gameObject);
            CoinScoreCalculator(other);
        }
        else if (GM.acquireMagnet == false &&
            other.gameObject.GetComponent<Renderer>() != null &&
            other.gameObject.GetComponent<Renderer>().material.color != gameObject.GetComponent<Renderer>().material.color &&
            (other.gameObject.tag != "door"))
        {
            Destroy(gameObject);
            doorScript.zVelPlayer = 0;
            Instantiate(gameOverAnimationObject, transform.position, gameOverAnimationObject.rotation);
            staticVars.gameStatus = "GameOver";
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

}
