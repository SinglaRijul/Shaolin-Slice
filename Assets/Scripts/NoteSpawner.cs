using System.Collections.Generic;

using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> arrowPrefabs;

    [SerializeField] List<Transform> hitPoints; 
    [SerializeField] List<Transform> spawnPoints; 
    [SerializeField] float arrowSpeed; 
    [SerializeField] float speedAdjustmentfactor = 0.9f;

    private int nextBeatIndex = 0;

    int currArrowId;
    BeatScroller beatManager;
    ConductorScript conductorScript;
    void Start() {

        beatManager = FindAnyObjectByType<BeatScroller>();
        conductorScript = FindAnyObjectByType<ConductorScript>();

    }

    void Update()
    {
        if (nextBeatIndex < beatManager.GetTotalBeats() && conductorScript.GetSongStarted())
        {

            float songPosition = conductorScript.GetSongPosition();

            //if (songPosition < 0.1f) return; 

            // Calculate travel time from spawn to hit point
            float distance = Vector3.Distance(spawnPoints[currArrowId].position, hitPoints[currArrowId].position);
        
            float timeToNextBeat = beatManager.GetBeatTimes()[nextBeatIndex] - songPosition;
            arrowSpeed = distance / Mathf.Max(timeToNextBeat, 0.01f) * speedAdjustmentfactor;

            float travelTime = Mathf.Min(distance / arrowSpeed, 1f);

            //Spawn the arrow ahead of time so it reaches the hit point at the correct moment
            if (songPosition >= beatManager.GetBeatTimes()[nextBeatIndex] - travelTime)
            {
                SpawnArrow();
                nextBeatIndex++;
            }
        }
    }

    void SpawnArrow()
    {
        currArrowId = GetRandomId();
        GameObject arrow = Instantiate(arrowPrefabs[currArrowId], spawnPoints[currArrowId].position, arrowPrefabs[currArrowId].transform.rotation, transform);
        arrow.GetComponent<NoteController>().speed = arrowSpeed;    
        //arrow.transform.position = new Vector3(arrow.transform.position.x,arrow.transform.position.y,-10f);
    }


    int GetRandomId()
    {
        int randomId = Random.Range(0,4);
        return randomId;

    }

    public void InitVariables()
    {
        nextBeatIndex = 0;
    }
}
