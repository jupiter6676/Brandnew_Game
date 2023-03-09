using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferSpawnManager : MonoBehaviour
{
    public static bool isSpawnTiming = false;   // 스폰해도 되는지 여부

    // Start is called before the first frame update
    void Start()
    {
        if (isSpawnTiming)
        {
            TransferManager theTM = FindObjectOfType<TransferManager>();

            isSpawnTiming = false;
            StartCoroutine(theTM.TransferDone());
        }
    }
}
