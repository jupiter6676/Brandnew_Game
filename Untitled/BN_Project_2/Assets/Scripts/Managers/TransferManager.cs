using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferManager : MonoBehaviour
{
    SplashManager theSplashManager; // 페이드인/아웃
    InteractionController theIC;    // UI 표시

    public static bool isFinished = true;  // 이동이 완전히 끝났는지

    public static TransferManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        theSplashManager = FindObjectOfType<SplashManager>();
        theIC = FindObjectOfType<InteractionController>();
    }

    public IEnumerator Transfer(string p_sceneName, string p_locationName)
    {
        isFinished = false;
        theIC.SettingUI(false); // UI 숨기기

        // 페이드아웃
        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeOut(false, true));
        yield return new WaitUntil(() => SplashManager.isFinished);

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

        isFinished = true;

        // 0.3초 정도 대기 후
        // isWaiting이 false면 대기중인 이벤트가 없다는 뜻이므로, 그때 UI를 표시한다.
        yield return new WaitForSeconds(0.3f);
        if (!DialogueManager.isWaiting)
            theIC.SettingUI(true);  // UI 보이기
    }
}
