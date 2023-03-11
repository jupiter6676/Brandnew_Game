using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log("DontDestroyOnLoad");
        }

        else
        {
            Destroy(gameObject);

            Debug.Log("Destroy");
        }
    }
}
