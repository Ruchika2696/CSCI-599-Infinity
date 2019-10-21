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
        yield return new WaitForSeconds(this.targetTime);
        GM.acquireMagnet = false;
        Debug.Log("Ended timer : " + this.timerName);
    }

    public IEnumerator ShieldPowerUp()
    {
        return null;
    }

 //   void timerEnded()
	//{
 //       GM.acquireMagnet = false;
 //       Debug.Log("Timer ended !!!!!!");
	//	//do your stuff here.
	//}

}
