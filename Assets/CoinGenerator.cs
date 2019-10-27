using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public ObjectPooler coinPool;
    public float distanceBetweenCoins = 2.0f;

    public void spawnCoins()
    {
        GameObject coin1 = coinPool.GetPooledObject();
        coin1.transform.position = new Vector3(0.53f , 0, 25.1f + distanceBetweenCoins);
        coin1.SetActive(true);

        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(0.53f , 0, 25.1f + distanceBetweenCoins);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3(0.53f , 0, 25.1f + distanceBetweenCoins);
        coin3.SetActive(true);
    }
}
