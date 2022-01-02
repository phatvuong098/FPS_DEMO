using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null) 
            {
                GameObject obj = new GameObject();
                obj.AddComponent<T>();
                obj.name = typeof(T).ToString();
                _instance = obj.GetComponent<T>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = GetComponent<T>();
        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }
}
