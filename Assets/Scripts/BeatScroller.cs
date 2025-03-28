using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    [SerializeField] float beatTempo;
    [SerializeField] bool hasStarted;

    List<float> beatTimes = new List<float>();

    [SerializeField] int totalBeats;
    void Start()
    {
        //LoadBeatTimes();
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
        string filepath =  Application.dataPath + $"/BeatMaps/{levelid}_beatmap.json";

        if(File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            BeatData beatData = JsonUtility.FromJson<BeatData>(json);
            beatTimes = beatData.beats;
            Debug.Log($"Loaded beats from {filepath}");

        }
        else
        {
            Debug.Log($"File not found!");
        }
        
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

//0.06965986394557823, 0.46439909297052157, 0.8823582766439909, 1.3003174603174603, 1.7182766439909296, 2.136235827664399, 2.5309750566893423, 2.948934240362812, 3.3668934240362813, 3.7616326530612243, 4.179591836734694, 4.574331065759637, 4.992290249433107, 5.410249433106576, 5.828208616780046, 6.246167800453515, 6.664126984126984, 7.082086167800454, 7.476825396825397, 7.894784580498866, 8.289523809523809, 8.70748299319728,