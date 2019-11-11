using UnityEngine;
using System.Collections;

public class SimpleTimer : MonoBehaviour
{

    //public float targetTime = 30.0f; // 60 seconds
    public float targetTime;
    public string timerName;


    public SimpleTimer(string timerName = null, float targetTime = 0.0f)
    {
        this.timerName = timerName;
        this.targetTime = targetTime;
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
        Debug.Log(GM.acquireMagnet);
        yield return new WaitForSeconds(this.targetTime);
        GM.acquireMagnet = false;
        Debug.Log("magnet acquire end!!");

        GM.powerupEffect = true;
        yield return new WaitForSeconds(3.0f);
        GM.powerupEffect = false;
        Debug.Log("magnet effect end!!");
    }

    public IEnumerator ShieldPowerUp()
    {
        GM.shieldMode = true;
        yield return new WaitForSeconds(this.targetTime);
        GM.shieldMode = false;
        //Debug.Log("Ended Shield timer : " + this.timerName);
    }

    //   void timerEnded()
    //{
    //       GM.acquireMagnet = false;
    //       Debug.Log("Timer ended !!!!!!");
    //	//do your stuff here.
    //}

}
