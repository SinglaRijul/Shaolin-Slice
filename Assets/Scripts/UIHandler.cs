using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{

    [SerializeField] GameObject levelSelectorObj;
    [SerializeField] GameObject pauseMenuObj;
    [SerializeField] GameObject pauseButton;

    ConductorScript conductorScript;

    void Awake()
    {
        conductorScript = FindAnyObjectByType<ConductorScript>();
        ShowLevelSelector();
    }

    void Update()
    {
        
    }



    public void ShowLevelSelector()
    {
        if(!levelSelectorObj.activeInHierarchy) levelSelectorObj.SetActive(true);
        pauseButton.SetActive(false); 
    }

    public void HideLevelSelector(int levelId)
    {
        if(levelSelectorObj.activeInHierarchy) levelSelectorObj.SetActive(false);

        if(conductorScript==null) return;
        pauseButton.SetActive(true);
        StartCoroutine(conductorScript.StartSongWithSync(levelId));

    }

    public void OnClickBacktoMenuButton()
    {
        //load start scene  
        SceneManager.LoadScene(0);
    }


    public void SetPauseMenu()
    {
        pauseMenuObj.SetActive(!pauseMenuObj.activeInHierarchy);
    }
}
