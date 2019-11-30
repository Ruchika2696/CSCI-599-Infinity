using UnityEngine;
using System.Collections;

public class SimpleTimer : MonoBehaviour
{

    //public float targetTime = 30.0f; // 60 seconds
    public float targetTime;
    public string timerName;
    private GameObject tutorialManager;

    public SimpleTimer(string timerName = null, float targetTime = 0.0f)
    {
        this.timerName = timerName;
        this.targetTime = targetTime;
    }

    private void Start()
    {
        tutorialManager = GameObject.Find("TutorialManager");
    }

    //   public void timerStart()
    //   {
    //       Update();
    //   }

    //void Update()
    //{

    //	targetTime -= Time.deltaTime;

    //	if (targetTime <= 0.0f)
    //	{
    //		timerEnded();
    //	}

    //}

    public IEnumerator MagnetPowerUp()
    {
        GM.acquireMagnet = true;
        yield return new WaitForSeconds(this.targetTime);
        GM.acquireMagnet = false;
        //Debug.Log("magnet acquire end!!");

        GM.powerupEffect = true;
        yield return new WaitForSeconds(3.0f);
        GM.powerupEffect = false;
        //Debug.Log("magnet effect end!!");
    }

    public IEnumerator ShieldPowerUp()
    {
        GM.shieldMode = true;
        yield return new WaitForSeconds(this.targetTime);
        GM.shieldMode = false;
        //Debug.Log("Ended Shield timer : " + this.timerName);
    }

    public IEnumerator DisplayTutorial()
    {
        //tutorialManager.GetComponent<TutorialManager>().displayTutorial = true;
        TutorialManager.Instance.displayTutorial = true;
        yield return new WaitForSeconds(this.targetTime);
        TutorialManager.Instance.displayTutorial = false;
        //tutorialManager.GetComponent<TutorialManager>().displayTutorial = false;
        //tutorialManager.GetComponent<TutorialManager>().Instance.completedTutorial();
        TutorialManager.Instance.completedTutorial();
    }

    //   void timerEnded()
    //{
    //       GM.acquireMagnet = false;
    //       Debug.Log("Timer ended !!!!!!");
    //	//do your stuff here.
    //}

}
