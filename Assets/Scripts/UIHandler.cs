using UnityEngine;

public class UIHandler : MonoBehaviour
{

    [SerializeField] GameObject levelSelectorObj;
    void Start()
    {
        
    }


    void Update()
    {
        
    }



    public void ShowLevelSelector()
    {
        if(!levelSelectorObj.activeInHierarchy) levelSelectorObj.SetActive(true); 
    }

    public void HideLevelSelector()
    {
        if(levelSelectorObj.activeInHierarchy) levelSelectorObj.SetActive(false);
    }

    
}
