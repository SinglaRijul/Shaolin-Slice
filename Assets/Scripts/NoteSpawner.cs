using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> arrowPrefabs;
    BeatScroller beatManager;
    ConductorScript conductorScript;
    [SerializeField] List<Transform> hitPoints; 
    [SerializeField] List<Transform> spawnPoints; 
    [SerializeField] float arrowSpeed; 


    private int nextBeatIndex = 0;

    int currArrowId;

    void Start() {

        beatManager = FindAnyObjectByType<BeatScroller>();
        conductorScript = FindAnyObjectByType<ConductorScript>();

    }

    void Update()
    {
        if (nextBeatIndex < beatManager.GetTotalBeats())
        {
            float songPosition = conductorScript.GetSongPosition();

            // Calculate travel time from spawn to hit point
            float distance = Vector3.Distance(spawnPoints[currArrowId].position, hitPoints[currArrowId].position);
            float travelTime = distance / arrowSpeed;
            // Debug.Log($"Song Position: {songPosition}, Next Beat Time: {beatManager.GetBeatTimes()[nextBeatIndex]}, Travel Time: {travelTime} , SongPosInBeats : {conductorScript.GetSongPosition()}");

            // Spawn the arrow ahead of time so it reaches the hit point at the correct moment
            if (songPosition >= beatManager.GetBeatTimes()[nextBeatIndex] - travelTime)
            {
                 //Debug.Log($"Spawning Arrow for Beat Index {nextBeatIndex}");
                SpawnArrow();
                nextBeatIndex++;
            }
        }
    }

    void SpawnArrow()
    {
        currArrowId = GetRandomId();
        GameObject arrow = Instantiate(arrowPrefabs[currArrowId], spawnPoints[currArrowId].position, arrowPrefabs[currArrowId].transform.rotation , transform);
        arrow.GetComponent<NoteController>().speed = arrowSpeed;    
    }


    int GetRandomId()
    {
        int randomId = Random.Range(0 , 3);
        return randomId;

    }
}
