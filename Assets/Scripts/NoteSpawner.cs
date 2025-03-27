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
        if (nextBeatIndex < beatManager.GetTotalBeats() && conductorScript.GetSongStarted())
        {
            float songPosition = conductorScript.GetSongPosition();

            // Calculate travel time from spawn to hit point
            float distance = Vector3.Distance(spawnPoints[currArrowId].position, hitPoints[currArrowId].position);
            float travelTime = distance / arrowSpeed;
            //Debug.Log($"Song Position: {songPosition}, Next Beat Time: {beatManager.GetBeatTimes()[nextBeatIndex]}, Travel Time: {travelTime} , SongPosInBeats : {conductorScript.GetSongPosition()}");

           

            //if travel time  is larger than the next beat time
            // if(Mathf.Abs(travelTime -beatManager.GetBeatTimes()[nextBeatIndex]) > Mathf.Epsilon)
            // {
            //     //recalculate distance
            //     distance = arrowSpeed * (beatManager.GetBeatTimes()[nextBeatIndex]- songPosition);
            //     spawnPos = hitPoints[currArrowId].position + Vector3.up * distance;
            //     Debug.Log($"new spawn pos {spawnPos}");

            //     SpawnArrow(spawnPos);
            //     return;

            // }


            // Spawn the arrow ahead of time so it reaches the hit point at the correct moment
            if (songPosition >= beatManager.GetBeatTimes()[nextBeatIndex] - travelTime)
            {
                Debug.Log($"Spawning Arrow {currArrowId}");
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
        int randomId = Random.Range(0,3);
        return randomId;

    }
}
