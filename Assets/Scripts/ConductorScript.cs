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

 
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        secsPerBeat = 60f/ songsBpm;

        secsPassedSinceStart = (float)AudioSettings.dspTime;

        audioSource.Play();

    }

    void Update()
    {   
        songPosition = (float)(AudioSettings.dspTime - secsPassedSinceStart);

        songPosInBeats = songPosition/secsPerBeat;
        



    }
}
