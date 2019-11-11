using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        staticVars.playCount++;
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Analytics.CustomEvent("PlayGame", new Dictionary<string, object>
        {
            { "PlayCount", staticVars.playCount }
        });
        Application.LoadLevel(1);
    }

    public void QuitGame()
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
