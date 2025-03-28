using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour
{
    string mainSceneName = "MainScene";

    [SerializeField] List<AudioClip> clickSounds;
    [SerializeField] Image fadePanel;  
    [SerializeField] float fadeDuration = 1.5f;  

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fadePanel.color = new Color(0, 0, 0, 0);
    }

    public void OnClickPlay()
    {
        PlayClickSound();
        StartCoroutine(LoadSceneWithFade(mainSceneName));
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
        audioSource.PlayOneShot(clickSounds[Random.Range(0, clickSounds.Count)], 0.8f);
    }

    IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Fade in (black screen appears)
        fadePanel.gameObject.SetActive(true);
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadePanel.color =  new Color(0,0,0,timer / fadeDuration);
            yield return null;
        }

        

        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        
    }
}
