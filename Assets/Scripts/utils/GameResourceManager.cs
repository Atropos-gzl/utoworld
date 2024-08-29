using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameResourceManager : MonoBehaviour
{
    private static Dictionary<string, Sprite> SpritesDic;
    private static Dictionary<string, GameObject> PrefabsDic;

    void Awake()
    {
        
        SpritesDic = new Dictionary<string, Sprite>();
        PrefabsDic = new Dictionary<string, GameObject>();

    }

    public static Sprite getSprite(string key) { 

        if (SpritesDic.ContainsKey(key)) return SpritesDic[key];
        else
        {
            Sprite sp =  Resources.Load<Sprite>("Sprite/"+key);
            SpritesDic.Add(key, sp);
            return sp;
        }
    }

    public static GameObject getPrefabs(string key)
    {
        if (PrefabsDic.ContainsKey(key))
        {
            return PrefabsDic[key];
        }
            
        else
        {
            GameObject go = Resources.Load<GameObject>("Prefabs/" + key);
            PrefabsDic.Add(key, go);
            return go;
        }
    }



}
