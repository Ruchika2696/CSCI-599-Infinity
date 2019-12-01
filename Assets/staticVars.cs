using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;

public class staticVars : MonoBehaviour
{
    // Start is called before the first frame update
    public static float yVel = 0;
    public static float gameTime = 0;
    public static float loadingTime = 0;
    public static int playCount = 0;
    public static string gameStatus = "";
    public static int quitCount = 0;
    public static bool paisa;
    public static int redCount, yellowCount, greenCount, score, shieldCount, magnetCount;
    GameObject redCountPos, greenCountPos, yellowCountPos, scorePos;
    GameObject timer;
    public static int speedTimer;
    public int speedIncrements;  // to stop speed inc after some time
    public DeathMenu deathScreen;
    public PauseMenu pauseScreen;

    public static bool doorTutorialTriggered;

    void Start()
    {
        redCountPos = GameObject.Find("redCount");
        yellowCountPos = GameObject.Find("yellowCount");
        greenCountPos = GameObject.Find("greenCount");
        scorePos = GameObject.Find("Score");
        if(deathScreen != null && deathScreen.gameObject != null)
        {
            if (deathScreen.gameObject.activeSelf == true)
            {
                timer = GameObject.Find("Timer");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if(gameStatus != "GameOver" && gameStatus != "Paused")
            speedTimer += 1;
        if(speedTimer > 400 && speedIncrements < 5
            && gameStatus != "GameOver" && gameStatus != "Paused")
        { 
            doorScript.zVelPlayer += 0.05f;
            speedTimer = 0;
            speedIncrements += 1;
        }
        if (gameStatus == "GameOver")
        {	
			Analytics.CustomEvent("coinsEarned", new Dictionary<string, object>
              {
                              { "yellowCount", staticVars.yellowCount },
                              { "greenCount", staticVars.greenCount },
                              { "redCount", staticVars.redCount }
              });

              Analytics.CustomEvent("powerUpEvent", new Dictionary<string, object>
               {
                              { "shieldCount", staticVars.shieldCount },
                              { "magnetCount", staticVars.magnetCount }
               });
			
            timer = GameObject.Find("Timer");
            int cur = (int)(10 - loadingTime);
            if (timer)
            {
                if (cur % 2 == 1)
                {
                    timer.GetComponent<TextMeshProUGUI>().text = ((cur + 1) / 2).ToString();
                }
            }
            loadingTime += Time.deltaTime;
            //Debug.Log("LOADING TIME :- " + loadingTime);
            if (loadingTime > (paisa ? 10 : 2))
            {
                gameStatus = "";
                loadingTime = 0;
                // SceneManager.LoadScene(0);
                doorScript.zVelPlayer = 1.0f;
                Application.LoadLevel(0);
            }
        }

        redCountPos.GetComponent<TextMeshProUGUI>().text = redCount.ToString();
        yellowCountPos.GetComponent<TextMeshProUGUI>().text = yellowCount.ToString();
        greenCountPos.GetComponent<TextMeshProUGUI>().text = greenCount.ToString();
        scorePos.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
