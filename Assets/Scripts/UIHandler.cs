using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{

    [SerializeField] GameObject levelSelectorObj;

    ConductorScript conductorScript;

    void Awake()
    {
        conductorScript = FindAnyObjectByType<ConductorScript>();
    }

    void Update()
    {
        
    }



    public void ShowLevelSelector()
    {
        if(!levelSelectorObj.activeInHierarchy) levelSelectorObj.SetActive(true); 
    }

    public void HideLevelSelector(int levelId)
    {
        if(levelSelectorObj.activeInHierarchy) levelSelectorObj.SetActive(false);

        if(conductorScript==null) return;
        StartCoroutine(conductorScript.StartSongWithSync(levelId));

    }

    public void OnClickBacktoMenuButton()
    {
        //load start scene  
        SceneManager.LoadScene(0);
    }

}
