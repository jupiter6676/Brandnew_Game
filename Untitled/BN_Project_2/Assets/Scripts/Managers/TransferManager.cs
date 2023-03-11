using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferManager : MonoBehaviour
{
    SplashManager theSplashManager; // ���̵���/�ƿ�
    InteractionController theIC;    // UI ǥ��

    public static bool isFinished = true;  // �̵��� ������ ��������

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
        theIC.SettingUI(false); // UI �����

        // ���̵�ƿ�
        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeOut(false, true));
        yield return new WaitUntil(() => SplashManager.isFinished);

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

        isFinished = true;

        // 0.3�� ���� ��� ��
        // isWaiting�� false�� ������� �̺�Ʈ�� ���ٴ� ���̹Ƿ�, �׶� UI�� ǥ���Ѵ�.
        yield return new WaitForSeconds(0.3f);
        if (!DialogueManager.isWaiting)
            theIC.SettingUI(true);  // UI ���̱�
    }
}
