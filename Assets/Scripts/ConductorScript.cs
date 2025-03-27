using System.Collections;
using System.Collections.Generic;
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

    int currentLevel;

    [SerializeField] Sprite playerIdleSprite;

    [SerializeField] List<Sprite> playerSprites;

    [SerializeField] SpriteRenderer npcSR;
    [SerializeField] SpriteRenderer playerSR;

    [SerializeField] List<LevelConfigSO> levelConfigs;

    [SerializeField] List<AudioClip> levelAudios;

    BeatScroller beatScrollerScript;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        beatScrollerScript = FindAnyObjectByType<BeatScroller>();


        secsPerBeat = 60f/ songsBpm;

        
        // audioSource.Play();

        // secsPassedSinceStart = (float)AudioSettings.dspTime;
        //StartCoroutine(StartSongWithSync());
        

    }

    void Update()
    {   
        //songPosition = audioSource.time;
        //Debug.Log($"song position : {songPosition}");
        
        if(!songStarted) return;

        songPosition = (float)(AudioSettings.dspTime - secsPassedSinceStart);

        songPosInBeats = songPosition/secsPerBeat;

    }


    public float GetSongPosition()
    {
        return songPosition;
        //return songPosInBeats;
    }
    public IEnumerator StartSongWithSync(int levelId)
    {
        currentLevel = levelId;

        InitSprites();

        // Start the audio
        //audioSource.clip = levelAudios[levelId];
        
        //calculation
        float songDuration = audioSource.clip.length;
        //Debug.Log($"song duration: {songDuration}");

        beatScrollerScript.LoadBeatData(levelId);
        songsBpm = (beatScrollerScript.GetTotalBeats()/songDuration)*60f;
        secsPerBeat = 60f/songsBpm;
        beatScrollerScript.SetBeatTempo(songsBpm/60f);
        
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


    
    public void InitSprites()
    {
        if(playerSR || npcSR == null) return;
        
        //player idle sprite
        playerSR.sprite = playerIdleSprite;

        //npc idle sprite
        npcSR.sprite = levelConfigs[currentLevel].GetNpcIdleSprite();

    }

    public void SetNpcSprite(int index)
    {
        // 0 means idle
        if(index == 0) npcSR.sprite = levelConfigs[currentLevel].GetNpcIdleSprite();
        else
        {
            npcSR.sprite = levelConfigs[currentLevel].GetNpcSpriteAt(index-1);

        }
    }
    public void SetPlayerSprite(int index)
    {
        // 0 means idle
        if(index == 0) playerSR.sprite = playerIdleSprite;
        else
        {
            playerSR.sprite = playerSprites[index-1];

        }
    }

    public bool GetSongStarted() => songStarted;

}
