using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    int currentLevel = -1;

    [SerializeField] Sprite playerIdleSprite;

    [SerializeField] List<Sprite> playerSprites;

    [SerializeField] SpriteRenderer npcSR;
    [SerializeField] SpriteRenderer playerSR;

    [SerializeField] List<LevelConfigSO> levelConfigs;

    [SerializeField] List<AudioClip> levelAudios;

    BeatScroller beatScrollerScript;
    NoteSpawner noteSpawner;

    int score =0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI hitText;

    UIHandler uiHandler;


    Animator anim;

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        beatScrollerScript = FindAnyObjectByType<BeatScroller>();
        anim = GetComponent<Animator>();
        uiHandler = FindAnyObjectByType<UIHandler>();
        noteSpawner = FindAnyObjectByType<NoteSpawner>();

        
        secsPerBeat = 60f/ songsBpm;

    }

    void Update()
    {   
        //songPosition = audioSource.time;
        //Debug.Log($"song position : {songPosition}");
        
        if(!audioSource.isPlaying && currentLevel!=-1 )
        {
            uiHandler.SetGameStatus(true , score);
            currentLevel=-1;
        }

        if(!songStarted) return;

        songPosition = (float)(AudioSettings.dspTime - secsPassedSinceStart);
        //songPosition = audioSource.time;

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
        InitScore();
        noteSpawner.InitVariables();
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


    public int GetScore() => score;

    public void AddScore(int add) 
    {
        //anim.SetBool("hasScored" , true);
        score += add;
        
    }

    void InitScore()
    {
        score = 0;
        SetScoreText();
    }

    public void SetScoreText() => scoreText.text = score.ToString();
    
    //public void ResetScoreAnimation() => anim.SetBool("hasScored" , false);

    public void SetHitText(string text) => hitText.text = text;
}
