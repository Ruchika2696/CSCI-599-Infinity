using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class staticVars : MonoBehaviour
{
    // Start is called before the first frame update

    public static float yVel = 0;
    public static float gameTime = 0;
    public static float loadingTime = 0;
    public static string gameStatus = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if(gameStatus == "GameOver")
        {
            loadingTime += Time.deltaTime;
        }

        if (loadingTime > 2)
        {
            SceneManager.LoadScene("GameResult");
        }
    }
}
