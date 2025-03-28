using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{

    [SerializeField] GameObject levelSelectorObj;
    [SerializeField] GameObject pauseMenuObj;
    [SerializeField] GameObject pauseButton;

    [SerializeField] GameObject levelOverObj;
    [SerializeField] TextMeshProUGUI levelOverText;

    ConductorScript conductorScript;

    bool isLevelOver;

    int finalScore= 0;

    void Awake()
    {
        conductorScript = FindAnyObjectByType<ConductorScript>();
        ShowLevelSelector();
        isLevelOver = false;
    }

    void Update()
    {
        if(isLevelOver)
        {
            ShowLevelSelector();
            SetLevelOverObj();
            levelOverText.text = $"FINAL SCORE \n{finalScore} \n You did great!";
            isLevelOver = false;
            
        }
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


    public void SetLevelOverObj()
    {
        levelOverObj.SetActive(!levelOverObj.activeInHierarchy);
        
    }

    public void OnClickBackButton()
    {
        SetLevelOverObj();
    }
    public void OnClickBacktoMenuButton()
    {
        //load start scene  
        SceneManager.LoadScene(0);
    }

    public void OnClickBackToLevelSelect()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        
        SceneManager.LoadScene(1);
    }

    public void SetPauseMenu()
    {
        pauseMenuObj.SetActive(!pauseMenuObj.activeInHierarchy);
        Time.timeScale = pauseMenuObj.activeInHierarchy ? 0 : 1;
        AudioListener.pause = pauseMenuObj.activeInHierarchy ? true : false;
    }

    public void SetGameStatus(bool flag , int finalScore){
        isLevelOver = flag;
        this.finalScore = finalScore;
    }
    
    public bool GetGameStatus() => isLevelOver;
}
