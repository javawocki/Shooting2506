using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceFinder : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<T> FindObjectsOfInterface<T>() where T : class
    {
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        List<T> list = new List<T>();

        foreach (var obj in allObjects) {
            if (obj is T interfaceObj)
            {
                list.Add(interfaceObj);
            }
        }

        return list;
    }
}
