using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    private static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (cache.ContainsKey(name))
        {
            if (Debug.isDebugBuild) Debug.LogWarning("Object [" + name + "] exists. Destroy new one");
            DestroyImmediate(gameObject);
        }
        else
            cache[name] = gameObject;
    }
}