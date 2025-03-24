using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour
{

    string mainSceneName = "MainScene";

    void Start()
    {
        
    }

    void Update()
    {
        
    }



    public void OnClickPlay()
    {
        SceneManager.LoadScene(mainSceneName);
    }
    
    public void OnClickCredits()
    {

    }

    public void OnClickQuit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
   
    }



}
