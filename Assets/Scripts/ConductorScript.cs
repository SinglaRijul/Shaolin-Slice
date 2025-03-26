using System.Collections;
using UnityEngine;

public class ConductorScript : MonoBehaviour
{
    
    [SerializeField]
    float songsBpm;

    [SerializeField]
    float secsPerBeat;

    [SerializeField]
    float songPosition;

    [SerializeField]
    float songPosInBeats;

    [SerializeField]
    float secsPassedSinceStart;

    bool songStarted = false;
 
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        secsPerBeat = 60f/ songsBpm;

        
        // audioSource.Play();

        // secsPassedSinceStart = (float)AudioSettings.dspTime;
        StartCoroutine(StartSongWithSync());
        

    }

    void Update()
    {   
        // songPosition = audioSource.time;
        Debug.Log($"song position : {songPosition}");
        
        if(!songStarted) return;

        songPosition = (float)(AudioSettings.dspTime - secsPassedSinceStart);

        songPosInBeats = songPosition/secsPerBeat;
        



    }


    public float GetSongPosition()
    {
        return songPosition;
        //return songPosInBeats;
    }
    IEnumerator StartSongWithSync()
    {
        // Start the audio
        audioSource.Play();
        
        // Wait until the audio source actually starts playing
        while (audioSource.time <= 0)
        {
            yield return null;
        }

        // Now record the accurate start time
        secsPassedSinceStart = (float)AudioSettings.dspTime - audioSource.time;
        songStarted = true;
    }
}
