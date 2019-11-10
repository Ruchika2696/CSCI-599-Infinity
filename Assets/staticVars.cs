using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class staticVars : MonoBehaviour
{
    // Start is called before the first frame update

    public static float yVel = 0;
    public static float gameTime = 0;
    public static float loadingTime = 0;
    public static string gameStatus = "";
    public static int redCount, yellowCount, greenCount, score;
    GameObject redCountPos, greenCountPos, yellowCountPos, scorePos;
    GameObject timer;
    public DeathMenu deathScreen;
    void Start()
    {
        redCountPos = GameObject.Find("redCount");
        yellowCountPos = GameObject.Find("yellowCount");
        greenCountPos = GameObject.Find("greenCount");
        scorePos = GameObject.Find("Score");
        if(deathScreen.gameObject.activeSelf == true)
        {
            timer = GameObject.Find("Timer");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameStatus == "GameOver")
        {
            timer = GameObject.Find("Timer");
            Debug.Log(timer);
            int cur = (int)(10 - loadingTime);
            if (cur % 2 == 1)
            {
                timer.GetComponent<TextMeshProUGUI>().text = ((cur + 1) / 2).ToString();
            } 
            loadingTime += Time.deltaTime;
            if (loadingTime > 10)
            {
                gameStatus = "";
                loadingTime = 0;
                // SceneManager.LoadScene(0);
                doorScript.zVelPlayer = 1;
                Application.LoadLevel(0);
            }
        }

        redCountPos.GetComponent<TextMeshProUGUI>().text = redCount.ToString();
        yellowCountPos.GetComponent<TextMeshProUGUI>().text = yellowCount.ToString();
        greenCountPos.GetComponent<TextMeshProUGUI>().text = greenCount.ToString();
        scorePos.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
