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
    public float timer = 0.0f;
//    private List<GameObject> activeTracks;
//    private int noOfTracksSpawnedInitial = 15;
//    private int noOfTracksSpawned = 15;
//    private int noOfTracksSpawnedPlainCubePref = 5;
//    private int noOfTracksSpawnedCubeGRPPrefMid = 4;

    private int lastPrefabIndex = 0;

    public ObjectPooler[] theObjectPools;

//    public CoinGenerator theCoinGenerator;


    // Start is called before the first frame update
    void Start()
    {
//        activeTracks = new List<GameObject>();


        playerTransform = GameObject.FindGameObjectWithTag("player").transform;
        for(int i=0; i<1; i++)
              SpawnTrack(0);
//        for(int i=0; i<noOfTracksSpawned; i++)
//            SpawnTrack();
//        theCoinGenerator = FindObjectOfType<CoinGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null)
        {
            if (playerTransform.position.z - safeZone > (spawnZ - tracksOnScreen * trackLength))
            {
//                SpawnTrack();
//                SpawnTrack(0);

                if(timer > 0.5f)
                {
                    SpawnTrack(1);
                        for(int i=0; i<1; i++)
                            SpawnTrack(0);
                    timer = 0.0f;
                }
                timer += Time.deltaTime;
                Debug.Log(timer);

                int prefabIndex = RandomPrefabIndex();

//                if(prefabIndex == 3) //DoorPrefab Spawn Once
//                {
//                    SpawnTrack(prefabIndex);
//                    for(int i=0; i<1; i++)
//                        SpawnTrack(0);
//                }
                if(prefabIndex != 1)
                {

                    for(int i=0; i<2; i++)
                    {
                       SpawnTrack(prefabIndex);
                    }

                   if(prefabIndex == 0){ //plane tracks
                        for(int i=0; i<3; i++)
                        {
                            SpawnTrack(prefabIndex);
                        }
                    }
                    else if(prefabIndex >= 10 ){ //All coins
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



//                theCoinGenerator.spawnCoins();
//
//                if(prefabIndex == 3)
//                {
//                    // prefab for basic Door
//
//                    // spawn CubeGRPPref before the basic Door prefab
//                    for(int i=0; i<noOfTracksSpawnedCubeGRPPrefMid; i++)
//                        SpawnTrack(1);
//
//                    // before door spawn plain cubePref to allow to move player for choosing door
//                    for (int i = 0; i < 3; i++)
//                        SpawnTrack(0);
//
//                    SpawnTrack(prefabIndex);
//                }
//                if(prefabIndex == 1 || prefabIndex == 2)
////                else
//                {
//                    for (int i = 0; i < noOfTracksSpawned; i++)
//                    {
//                        SpawnTrack(prefabIndex);
//                    }
//                }
//                if(prefabIndex != 0)
//                {
//                    // if the prefab just spawned is not a PlainCubePref
//                    // spawn a few instances of PlainCubePref
//                    for(int i=0; i< noOfTracksSpawnedPlainCubePref; i++)
//                        SpawnTrack(0);
//                }
//                DeleteTrack();
//                //            coin_gen.SpawnCoins(new Vector3(2.5f, -4.5f, -24.01f));
            }
        }
    }

    private void SpawnTrack(int prefabIndex = 0)
    {
        GameObject go;
//        // go = Instantiate(trackPrefabs[1]) as GameObject;
//        //go = Instantiate(trackPrefabs[RandomPrefabIndex()]) as GameObject;
        go = theObjectPools[prefabIndex].GetPooledObject();
//        go = Instantiate(trackPrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(0.5f, 0f, spawnZ);
        spawnZ += trackLength;
////        activeTracks.Add(go);
        go.SetActive(true);
    }

    private void DeleteTrack()
    {
          gameObject.SetActive(false);
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
}
