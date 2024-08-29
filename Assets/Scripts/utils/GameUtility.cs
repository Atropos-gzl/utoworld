using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtility
{
    public static Transform FindChildWithName(Transform parent, string name)
    {
        Transform child = parent.Find(name);
        if (child == null)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                return FindChildWithName(parent.GetChild(i), name);
            }
        }
        else
        {
            return child;
        }
        return null;
    }
}
