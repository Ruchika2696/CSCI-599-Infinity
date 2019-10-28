using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
//    public GameObject[] trackPrefabs;
    private float spawnZ = 25.1f;
    private float trackLength = 2.0f;
    private Transform playerTransform;
    private float safeZone = 18f;
    private int tracksOnScreen = 100;
    public float doortimer = 0.0f;
    public float magnettimer = 0.0f;
    public float shieldtimer = 0.0f;

    private int lastPrefabIndex = 0;

    public ObjectPooler[] theObjectPools;
    List<GameObject> activeTracks;

    // Start is called before the first frame update
    void Start()
    {
        activeTracks = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;
        for(int i=0; i<3; i++)
              SpawnTrack(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null)
        {
            if (playerTransform.position.z - safeZone > (spawnZ - tracksOnScreen * trackLength))
            {
                if(doortimer > 0.2f) //generate door prefab for every 0.3f
                {
                    SpawnTrack(1);
                        for(int i=0; i<2; i++)
                            SpawnTrack(0);
                    doortimer = 0.0f;
                }
                doortimer += Time.deltaTime;

                if (magnettimer > 0.7f) //generate magnet prefab for every 0.3f
                {
                   
                    int magnetPrefabIndex = FindRandomPrefabIndexForMagnet();
                    SpawnTrack(magnetPrefabIndex);
                    for (int i = 0; i < 2; i++)
                        SpawnTrack(0);
                    magnettimer = 0.0f;
                }
                magnettimer += Time.deltaTime;

                if (shieldtimer > 0.8f) //generate shield prefab for every 0.3f
                {
                    int shieldPrefabIndex = FindRandomPrefabIndexForShield();
                    SpawnTrack(shieldPrefabIndex);
                    for (int i = 0; i < 2; i++)
                        SpawnTrack(0);
                    shieldtimer = 0.0f;

                }
                shieldtimer += Time.deltaTime;

                int prefabIndex = RandomPrefabIndex();

                if(prefabIndex != 1 && prefabIndex <20) 
                {

                    for(int i=0; i<2; i++)
                    {
                       SpawnTrack(0);
                    }

                   if(prefabIndex == 0){ //plane tracks
                        for(int i=0; i<3; i++)
                        {
                            SpawnTrack(prefabIndex);
                        }
                    }
                    else if(prefabIndex >= 10 && prefabIndex <20 ){ //All coins
                        for(int i=0; i<5; i++)
                        {
                            SpawnTrack(prefabIndex);
                        }
                    }
                    else { //obstacles and pits
                        for(int i=0; i<2; i++)
                            SpawnTrack(0);
                        SpawnTrack(prefabIndex);
                        for(int i=0; i<2; i++)
                            SpawnTrack(0);
                    }
                }
                if (activeTracks.Count > 120)
                    DeleteTrack();
            }
        }
    }

    private void SpawnTrack(int prefabIndex = 0)
    {
        GameObject go;
        if (theObjectPools[prefabIndex] != null)
        {
            go = theObjectPools[prefabIndex].GetPooledObject();
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(0.5f, 0f, spawnZ);
            spawnZ += trackLength;
            activeTracks.Add(go);
            go.SetActive(true);
        }
    }

    private void DeleteTrack()
    {
        GameObject obj = activeTracks[0];
        obj.SetActive(false);
        activeTracks.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
       if (theObjectPools.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, theObjectPools.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    private int FindRandomPrefabIndexForMagnet()
    {
        int formagnetindex = Random.Range(23, 26);
        return formagnetindex;
    }

    private int FindRandomPrefabIndexForShield()
    {
        int forshieldindex = Random.Range(20, 23);
        return forshieldindex;
    }
}
