using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferManager : MonoBehaviour
{
    SplashManager theSplashManager; // ���̵���/�ƿ�
    InteractionController theIC;    // UI ǥ��

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        theSplashManager = FindObjectOfType<SplashManager>();
        theIC = FindObjectOfType<InteractionController>();
    }

    public IEnumerator Transfer(string p_sceneName, string p_locationName)
    {
        theIC.SettingUI(false); // UI �����
        yield return null;
        //InteractionController.clickedInteractive = false;

        // ���̵�ƿ�
        //SplashManager.isFinished = false;
        //StartCoroutine(theSplashManager.FadeOut(false, true));
        //yield return new WaitUntil(() => SplashManager.isFinished);

        // �� ��ȯ
        TransferSpawnManager.isSpawnTiming = true;
        SceneManager.LoadScene(p_sceneName);
    }

    // �� �̵� �Ϸ�
    public IEnumerator TransferDone()
    {
        // ���̵���
        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeIn(false, true));
        yield return new WaitUntil(() => SplashManager.isFinished);

        theIC.SettingUI(true);  // UI ���̱�
    }
}
