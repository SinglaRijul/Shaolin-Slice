using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    [SerializeField] float beatTempo;
    [SerializeField] bool hasStarted;

    List<float> beatTimes = new List<float>();


    [SerializeField] List<TextAsset> beatMapFiles;
    [SerializeField] int totalBeats;

    void Start()
    {
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

    void LoadBeatTimes(string levelid)
    {
        //string filepath =  Application.dataPath + $"/BeatMaps/{levelid}_beatmap.json";
        // string filepath = Path.Combine(Application.streamingAssetsPath, $"BeatMaps/{levelid}_beatmap.json");

        // if(File.Exists(filepath))
        // {
        //     //string json = File.ReadAllText(filepath);
        //     BeatData beatData = JsonUtility.FromJson<BeatData>(json);
        //     beatTimes = beatData.beats;
        //     Debug.Log($"Loaded beats from {filepath}");

        // }
        // else
        // {
        //     Debug.Log($"File not found!");
        // }

        BeatData beatData = JsonUtility.FromJson<BeatData>(beatMapFiles[Convert.ToInt32(levelid)].text);
        beatTimes = beatData.beats;
        
    }


    public List<float> GetBeatTimes() => beatTimes;
    public int GetTotalBeats() => totalBeats;

    public void LoadBeatData(int levelid)
    {
        LoadBeatTimes(levelid.ToString());
        totalBeats = beatTimes.Count;
        //beatTempo = beatTempo/60f;  
    
    }

    public void SetBeatTempo(float beatTempo) => this.beatTempo = beatTempo;
    

}

[System.Serializable]
public class BeatData{
    public List<float> beats;
}
