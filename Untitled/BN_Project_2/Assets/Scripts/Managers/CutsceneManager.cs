using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public static bool isFinished = false;  // 컷신이 다 불러와졌는지

    SplashManager theSplashManager;

    [SerializeField] Image img_cutscene;


    void Start()
    {
        theSplashManager = FindObjectOfType<SplashManager>();
    }

    public bool CheckCutscene()
    {
        return img_cutscene.gameObject.activeSelf;  // 현재 이 객체가 활성화 상태인지 반환
    }

    public IEnumerator CutsceneCoroutine(string p_cutsceneName, bool p_isShow)
    {
        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeOut(true, false));      // 하얗고 빠르게
        yield return new WaitUntil(() => SplashManager.isFinished); // 페이드아웃 끝날 때까지 대기

        // 컷신을 보여준다.
        if (p_isShow)
        {
            p_cutsceneName = p_cutsceneName.Trim();
            Sprite t_sprite = Resources.Load<Sprite>("Images/Cutscenes/" + p_cutsceneName);

            if (t_sprite != null)
            {
                img_cutscene.gameObject.SetActive(true);
                img_cutscene.sprite = t_sprite;
            }
        }

        // 컷씬을 보여주지 않는다.
        else
        {
            img_cutscene.gameObject.SetActive(false);
        }

        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeIn(true, false));       // 하얗고 빠르게
        yield return new WaitUntil(() => SplashManager.isFinished); // 페이드인 끝날 때까지 대기

        yield return new WaitForSeconds(0.5f);  // 컷신 끝나고 0.5초 후 텍스트 출력

        isFinished = true;  // 컷신 불러오기 끝
    }
}
