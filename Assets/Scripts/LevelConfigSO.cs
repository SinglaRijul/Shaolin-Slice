using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] Sprite npcIdleSprite;
    [SerializeField] List<Sprite> npcSprites; //in order up ,  left , down , right

    public Sprite GetNpcSpriteAt(int index)
    {
        if(index>=0 && index < npcSprites.Count) return npcSprites[index];
        else return null;
    }

    public Sprite GetNpcIdleSprite()
    {
        return npcIdleSprite;
    }
    
}
