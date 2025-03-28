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
    [SerializeField] float speedAdjustmentfactor = 1f;

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
            //Debug.Log($"beat index {nextBeatIndex}");
            float songPosition = conductorScript.GetSongPosition();

            //if (songPosition < 0.1f) return; 

            // Calculate travel time from spawn to hit point
            float distance = Vector3.Distance(spawnPoints[currArrowId].position, hitPoints[currArrowId].position);
            //Debug.Log($"distance to travel {distance}");
            //float travelTime = distance / arrowSpeed;
            //Debug.Log($"Song Position: {songPosition}, Next Beat Time: {beatManager.GetBeatTimes()[nextBeatIndex]}, Travel Time: {travelTime} , SongPosInBeats : {conductorScript.GetSongPosition()}");

            float timeToNextBeat = beatManager.GetBeatTimes()[nextBeatIndex] - songPosition;
            arrowSpeed = distance / Mathf.Max(timeToNextBeat, 0.01f) * speedAdjustmentfactor;

            float travelTime = Mathf.Min(distance / arrowSpeed, 1f);
            //Debug.Log($"Song Position: {songPosition}, Next Beat Time: {beatManager.GetBeatTimes()[nextBeatIndex]}, Travel Time: {travelTime} , SongPosInBeats : {conductorScript.GetSongPosition()}");


            // float distance = 9f;
            // float timeToHit = beatManager.GetBeatTimes()[nextBeatIndex]-conductorScript.GetSongPosition();

            // if(timeToHit>0f && timeToHit<= 1f)
            // {
            //     arrowSpeed = distance/timeToHit;
            //     SpawnArrow();
            //     nextBeatIndex++;
            // }

            //Spawn the arrow ahead of time so it reaches the hit point at the correct moment
            if (songPosition >= beatManager.GetBeatTimes()[nextBeatIndex] - travelTime)
            {
                
                //Debug.Log($"Song Position: {songPosition}, Next Beat Time: {beatManager.GetBeatTimes()[nextBeatIndex]}, Travel Time: {travelTime} , SongPosInBeats : {conductorScript.GetSongPosition()}");

                //Debug.Log($"Spawning Arrow {currArrowId}");
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

    public void InitVariables()
    {
        nextBeatIndex = 0;
    }
}
