using UnityEngine;

public class NotSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;
    BeatScroller beatManager;
    ConductorScript conductorScript;
    public Transform hitPoint; 
    public Transform spawnPoint; 
    public float arrowSpeed; 
    private int nextBeatIndex = 0;

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
            float distance = Vector3.Distance(spawnPoint.position, hitPoint.position);
            float travelTime = distance / arrowSpeed;

            // Spawn the arrow ahead of time so it reaches the hit point at the correct moment
            if (songPosition >= beatManager.GetBeatTimes()[nextBeatIndex] - travelTime)
            {
                SpawnArrow();
                nextBeatIndex++;
            }
        }
    }

    void SpawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, arrowPrefab.transform.rotation , transform);
        arrow.GetComponent<NoteController>().speed = arrowSpeed;    
    }
}
