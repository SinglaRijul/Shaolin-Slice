using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConductorScript : MonoBehaviour
{
    [Header("Song Params")]
    [SerializeField]
    float songsBpm;

    [SerializeField]
    float secsPerBeat;

    [SerializeField]
    float songPosition;

    [SerializeField]
    float secsPassedSinceStart;

    bool songStarted = false;
    int currentLevel = -1;

    [Header("Player and Npc")]
    [SerializeField] Sprite playerIdleSprite;

    [SerializeField] List<Sprite> playerSprites;
    [SerializeField] SpriteRenderer playerSR;
    
    [SerializeField] GameObject npcObj;
    
    SpriteRenderer npcSR;
    
    [SerializeField] List<LevelConfigSO> levelConfigs;
    [SerializeField] GameObject bgObj;
    [SerializeField] List<Sprite> bgSprites;

    int score =0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI hitText;

    UIHandler uiHandler;

    bool isWaiting = false;
    Animator anim;
    AudioSource audioSource;

    BeatScroller beatScrollerScript;
    NoteSpawner noteSpawner;
    

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        beatScrollerScript = FindAnyObjectByType<BeatScroller>();
        anim = npcObj.GetComponent<Animator>();
        uiHandler = FindAnyObjectByType<UIHandler>();
        noteSpawner = FindAnyObjectByType<NoteSpawner>();
        npcSR = npcObj.GetComponent<SpriteRenderer>();
        
        currentLevel = -1;
        secsPerBeat = 60f/ songsBpm;

    }

    void Update()
    {     
        
        if(!audioSource.isPlaying && currentLevel!=-1 && !isWaiting)
        {
            if(AudioListener.pause){return;}

            //set game over
            isWaiting = false;      
            currentLevel=-1;
            uiHandler.SetGameStatus(true , score);
            
        }

        if(!songStarted) return;

        songPosition = (float)(AudioSettings.dspTime - secsPassedSinceStart);

    }

    public float GetSongPosition() => songPosition;

    public IEnumerator StartSongWithSync(int levelId)
    {
        currentLevel = levelId;
        songStarted = false;

        InitSprites();
        InitScore();
        noteSpawner.InitVariables();

        bgObj.GetComponent<SpriteRenderer>().sprite = bgSprites[levelId];
        
        // Start the audio
        audioSource.clip = levelConfigs[levelId].GetLevelAudio();
        

        if(currentLevel!=0)
        {
            //play animation
            anim.runtimeAnimatorController = levelConfigs[currentLevel].GetAnimControllerNpc();
        }

        //calculation
        float songDuration = audioSource.clip.length;

        beatScrollerScript.LoadBeatData(levelId);
        songsBpm = (beatScrollerScript.GetTotalBeats()/songDuration)*60f;
        secsPerBeat = 60f/songsBpm;
        beatScrollerScript.SetBeatTempo(songsBpm/60f);
        
        isWaiting = true;
        yield return new WaitForSeconds(1.5f);
        isWaiting= false;

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
        if(playerSR == null || npcSR == null || currentLevel==-1) return;
        
        //player idle sprite
        playerSR.sprite = playerIdleSprite;

        //npc idle sprite
        npcSR.sprite = levelConfigs[currentLevel].GetNpcIdleSprite();

    }

    public void SetNpcSprite(int index)
    {
        //only curr level = 0 has npc idle sprite
        if(currentLevel!=0) return;
        
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
