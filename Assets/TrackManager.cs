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
                for (int i = 0; i < noOfTracksSpawned; i++)
                {
                    SpawnTrack(prefabIndex);
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
