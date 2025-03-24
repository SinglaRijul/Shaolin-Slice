using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    [SerializeField] float beatTempo;
    [SerializeField] bool hasStarted;
    [SerializeField] TextAsset beatMap;

    List<float> beatTimes = new List<float>();

    [SerializeField] int totalBeats;
    void Start()
    {
        LoadBeatTimes();
        totalBeats = beatTimes.Count;


        beatTempo = beatTempo/60f;    
    }

    void Update()
    {
        
        if(!hasStarted)
        {
            if(Input.anyKeyDown)
            {
                hasStarted= true;
            }
        }
        else{

            transform.position -= new Vector3(0f,beatTempo*Time.deltaTime,0f);

        }



    }


    void LoadBeatTimes()
    {
        if(beatMap==null){return;}

        BeatData beatData = JsonUtility.FromJson<BeatData>(beatMap.text);
        beatTimes = beatData.beats;

        Debug.Log("Loaded beats");
    }


    public List<float> GetBeatTimes()
    {
        return beatTimes;
    }

    public int GetTotalBeats()
    {
        return totalBeats;
    }


}


[System.Serializable]
public class BeatData{
    public List<float> beats;
}