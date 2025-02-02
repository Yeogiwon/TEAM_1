using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static GameObject LoadUI(string uiName)
    {
        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{uiName}");
        if (go == null)
        {
            Debug.Log($"Failed to Load : {uiName}");
            return null;
        }
        return go;
    }

    public static GameObject LoadUnit(string unitName)
    {
        GameObject go = Resources.Load<GameObject>($"Prefabs/Units/{unitName}");
        if (go == null)
        {
            Debug.Log($"Failed to Load : {unitName}");
            return null;
        }
        return go;
    }

    public static Sprite LoadSprite(string spriteName) {
        Sprite sp = Resources.Load<Sprite>($"Sprites/{spriteName}");
        if (sp == null)
        {
            Debug.Log($"Failed to Load : {spriteName}");
            return null;
        }
        return sp;

    }
}
