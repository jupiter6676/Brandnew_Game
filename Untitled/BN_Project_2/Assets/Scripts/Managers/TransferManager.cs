using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferManager : MonoBehaviour
{
    public IEnumerator Transfer(string p_sceneName, string p_locationName)
    {
        yield return null;  // 한 프레임 대기

        SceneManager.LoadScene(p_sceneName);
    }
}
