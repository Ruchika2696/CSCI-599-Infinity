using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class moveBall : MonoBehaviour
{
	public bool flag = false;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode space;
	public float horVel = 0;
	public int laneNum = 2;
	int safeLane;
	public float newX = 0;
	public static string movementBlocked = "NO";
	public Transform gameOverAnimationObject;
	SimpleTimer st;

	private bool isGrounded;
	private bool groundContact;
	float gameTime = 0;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	public DeathMenu deathScreen;
	GameObject timer;
	float safeZ;

	// Start is called before the first frame update
	void Start()
	{
		//gameObject.GetComponent<Renderer>().material.color = Color.yellow;
		Material yellowMat = Resources.Load("centerDoor", typeof(Material)) as Material;
		gameObject.GetComponent<Renderer>().material = yellowMat;
		staticVars.redCount = 0;
		staticVars.greenCount = 0;
		staticVars.yellowCount = 0;
		staticVars.score = 0;
		safeZ = 0.78f;
		safeLane = 2;
        movementBlocked = "NO";
	}

	// Update is called once per frame
	void Update()
	{
		// Debug.Log("HEEEEEE");
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


			flag = false;
		}
		//Debug.Log("in update" + gameObject.transform.position);
		gameTime += Time.deltaTime;
		if (gameTime > 6)
		{
			safeZ = gameObject.transform.position.z;
			Debug.Log("SAFE Z VALUE :- " + safeZ);
			safeLane = laneNum;
            Debug.Log("AAAAAJAAJAJAJA");
            Debug.Log("SAFE LANE :- " + safeLane);
			gameTime = 0;
		}
		staticVars.score++;
		GetComponent<Rigidbody>().velocity = new Vector3(horVel, staticVars.yVel, 10 * doorScript.zVelPlayer);

		if (Input.GetKeyDown(moveLeft) && (laneNum > 1) && (movementBlocked == "NO"))
		{
			//    Debug.Log("HIIIIIIIIIIIIIIIIIIIIIIIII");
			horVel = -5;
			StartCoroutine(stopSlide());
			laneNum -= 1;
			movementBlocked = "YES";

		}
		if (Input.GetKeyDown(moveRight) && (laneNum < 3) && (movementBlocked == "NO"))
		{
			//    Debug.Log("HELLOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
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
			groundContact = true;
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

		isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);
		if (isGrounded && groundContact)
		{
			Debug.Log("hit space");
			GetComponent<Rigidbody>().AddForce(Vector3.up * 100, ForceMode.Impulse);
			groundContact = false;
			yield return new WaitForSeconds(.6f);
			GetComponent<Rigidbody>().AddForce(Vector3.down * 100, ForceMode.Impulse);
			groundContact = true;
		}

	}
	/*private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "danger")
		{
			//   Debug.Log("MACHAYENGE");
			Destroy(gameObject);
		}
	} */



	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Magnet"))
		{
			// when the player collides with a magnet. acquires magnet power-up
			//    Debug.Log("triggered on magnet contact");

			st = new SimpleTimer("MagnetTimer", 15.0f);
			StartCoroutine(st.MagnetPowerUp());
			// if collider is a magnet
			GM.acquireMagnet = true;
			//gameObject.tag = "MagnetPlayer";

			// destroy the magnet
			Destroy(other.gameObject);
		}

		if (other.tag == "danger")
		{
			//      Debug.Log("FALLING :- " + other.name);
			Instantiate(gameOverAnimationObject, transform.position, gameOverAnimationObject.rotation);
			doorScript.zVelPlayer = 0;
			//deathScreen.gameObject.SetActive(true);
			staticVars.gameStatus = "GameOver";
			//   Destroy(gameObject);
			gameObject.SetActive(false);
		}

		else if (GM.acquireMagnet == false &&
			other.gameObject.GetComponent<Renderer>() != null &&
			other.gameObject.GetComponent<Renderer>().material.color == gameObject.GetComponent<Renderer>().material.color &&
			(other.gameObject.tag != "door"))
		{
			//Debug.Log("first else if");
			Destroy(other.gameObject);
			Material yellowMat = Resources.Load("centerDoor", typeof(Material)) as Material;
			Material redMat = Resources.Load("leftDoor", typeof(Material)) as Material;
			Material greenMat = Resources.Load("rightDoor", typeof(Material)) as Material;
			if (other.gameObject.GetComponent<Renderer>().material.color == yellowMat.color)
				staticVars.yellowCount++;
			else if (other.gameObject.GetComponent<Renderer>().material.color == greenMat.color)
				staticVars.greenCount++;
			else if (other.gameObject.GetComponent<Renderer>().material.color == redMat.color)
				staticVars.redCount++;

		}
		else if (GM.acquireMagnet == false &&
			other.gameObject.GetComponent<Renderer>() != null &&
			other.gameObject.GetComponent<Renderer>().material.color != gameObject.GetComponent<Renderer>().material.color &&
			(other.gameObject.tag != "door"))
		{
			//Debug.Log("second else if");
			//   Destroy(gameObject);
			gameObject.SetActive(false);
			doorScript.zVelPlayer = 0;
			Instantiate(gameOverAnimationObject, transform.position, gameOverAnimationObject.rotation);
			//deathScreen.gameObject.SetActive(true);
			staticVars.gameStatus = "GameOver";
		}

		if (GM.acquireMagnet == true && other.tag == "Coin")
		{
			// when the player has magnet, on trigger, acquire coin
			// irrespective of coin and ball color
			Destroy(other.gameObject);
			Material yellowMat = Resources.Load("centerDoor", typeof(Material)) as Material;
			Material redMat = Resources.Load("leftDoor", typeof(Material)) as Material;
			Material greenMat = Resources.Load("rightDoor", typeof(Material)) as Material;
			if (other.gameObject.GetComponent<Renderer>().material.color == yellowMat.color)
				staticVars.yellowCount++;
			else if (other.gameObject.GetComponent<Renderer>().material.color == greenMat.color)
				staticVars.greenCount++;
			else if (other.gameObject.GetComponent<Renderer>().material.color == redMat.color)
				staticVars.redCount++;
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

	/*public void Reset()
	{

		//  if (staticVars.redCount >= 10 && staticVars.greenCount >= 10 && staticVars.yellowCount >= 10)
		if (true)
		{
			//     movementBlocked = "YES";
			GM.acquireMagnet = false;
			deathScreen.gameObject.SetActive(false);
			staticVars.gameStatus = "";
			staticVars.loadingTime = 0;
			doorScript.zVelPlayer = 1;
			gameObject.SetActive(true);
			horVel = 0;
			movementBlocked = "NO";
			if (safeLane == 1)
			{
			//	Debug.Log("SAFE Z VALUE :- " + safeZ);
				float y = gameObject.transform.position.y;
				float z = gameObject.transform.position.z;
				Vector3 newPos = new Vector3(-2f, 1.14f, safeZ);
				gameObject.transform.position = newPos;
			//	Debug.Log(gameObject.transform.position);

			}

			if (safeLane == 2)
			{
//				Debug.Log("SAFE#2 Z VALUE :- " + safeZ);
				float y = gameObject.transform.position.y;
				float z = gameObject.transform.position.z;
				float x = gameObject.transform.position.x;
		//		Debug.Log("X value :- " + x);
				Vector3 newPos = new Vector3(0.5f, 1.14f, safeZ);
				gameObject.transform.position = newPos;
		//		Debug.Log(gameObject.transform.position);
			}

			if (safeLane == 3)
			{
			//	Debug.Log("SAFE Z VALUE :- " + safeZ);
				float y = gameObject.transform.position.y;
				float z = gameObject.transform.position.z;
				Vector3 newPos = new Vector3(3f, 1.14f, safeZ);
				gameObject.transform.position = newPos;
//				Debug.Log(gameObject.transform.position);

			}
			staticVars.redCount = max(0, staticVars.redCount - 10);
			staticVars.greenCount = max(0, staticVars.greenCount - 10);
			staticVars.yellowCount = max(0, staticVars.yellowCount - 10);
			Vector3 changeCam = new Vector3(0.8f, 5f, (safeZ - 14.37f));
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

	} */
}
