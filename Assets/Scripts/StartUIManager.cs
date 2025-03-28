using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour
{

    string mainSceneName = "MainScene";

    [SerializeField] List<AudioClip> clickSounds;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void OnClickPlay()
    {
        PlayClickSound();
        SceneManager.LoadScene(mainSceneName);
        
    }
    
    public void OnClickCredits()
    {
        PlayClickSound();

    }

    public void OnClickQuit()
    {
        PlayClickSound();

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
   
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSounds[Random.Range(0 , clickSounds.Count )] , 0.8f);
    }



}
