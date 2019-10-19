using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public GameObject[] trackPrefabs;
    private float spawnZ = 25.1f;
    private float trackLength = 2.0f;
    private Transform playerTransform;
    private float safeZone = 18f;
    private int tracksOnScreen = 20;
    private List<GameObject> activeTracks;
    private int noOfTracksSpawnedInitial = 15;
    private int noOfTracksSpawned = 15;
    private int noOfTracksSpawnedPlainCubePref = 5;
    private int noOfTracksSpawnedCubeGRPPrefMid = 4;

    private int lastPrefabIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        activeTracks = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;
        for(int i=0; i<noOfTracksSpawned; i++)
            SpawnTrack();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null)
        {
            if (playerTransform.position.z - safeZone > (spawnZ - tracksOnScreen * trackLength))
            {
                int prefabIndex = RandomPrefabIndex();
                
                if(prefabIndex == 3)
                {
                    // prefab for basic Door 

                    // spawn CubeGRPPref before the basic Door prefab
                    for(int i=0; i<noOfTracksSpawnedCubeGRPPrefMid; i++)
                        SpawnTrack(1);

                    // before door spawn plain cubePref to allow to move player for choosing door
                    for (int i = 0; i < 3; i++)
                        SpawnTrack(0);

                    SpawnTrack(prefabIndex);
                }
                else
                {
                    for (int i = 0; i < noOfTracksSpawned; i++)
                    {
                        SpawnTrack(prefabIndex);
                    }
                }
                if(prefabIndex != 0)
                {
                    // if the prefab just spawned is not a PlainCubePref
                    // spawn a few instances of PlainCubePref
                    for(int i=0; i< noOfTracksSpawnedPlainCubePref; i++)
                        SpawnTrack(0);
                }
                DeleteTrack();
                //            coin_gen.SpawnCoins(new Vector3(2.5f, -4.5f, -24.01f));
            }
        }
    }

    private void SpawnTrack(int prefabIndex = 0)
    {
        GameObject go;
        // go = Instantiate(trackPrefabs[1]) as GameObject;
        //go = Instantiate(trackPrefabs[RandomPrefabIndex()]) as GameObject;
        go = Instantiate(trackPrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(0.5f, 0f, spawnZ);
        spawnZ += trackLength;
        activeTracks.Add(go);
    }

    private void DeleteTrack()
    {
        Destroy(activeTracks[0]);
        activeTracks.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
       if (trackPrefabs.Length <= 1)
            return 0;
        
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, trackPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
