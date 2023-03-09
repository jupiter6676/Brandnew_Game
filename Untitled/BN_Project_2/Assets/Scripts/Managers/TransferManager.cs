using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferManager : MonoBehaviour
{
    SplashManager theSplashManager; // 페이드인/아웃
    InteractionController theIC;    // UI 표시

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
        theIC.SettingUI(false); // UI 숨기기
        yield return null;
        //InteractionController.clickedInteractive = false;

        // 페이드아웃
        //SplashManager.isFinished = false;
        //StartCoroutine(theSplashManager.FadeOut(false, true));
        //yield return new WaitUntil(() => SplashManager.isFinished);

        // 신 전환
        TransferSpawnManager.isSpawnTiming = true;
        SceneManager.LoadScene(p_sceneName);
    }

    // 맵 이동 완료
    public IEnumerator TransferDone()
    {
        // 페이드인
        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeIn(false, true));
        yield return new WaitUntil(() => SplashManager.isFinished);

        theIC.SettingUI(true);  // UI 보이기
    }
}
