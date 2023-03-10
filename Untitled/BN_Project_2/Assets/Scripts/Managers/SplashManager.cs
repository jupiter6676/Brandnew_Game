using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [SerializeField] Image img; // 배경 이미지 → 얘를 페이드인/아웃

    [SerializeField] Color colorWhite;
    [SerializeField] Color colorBlack;

    [SerializeField] float fadeSpeed;
    [SerializeField] float fadeSlowSpeed;

    // 페이드되는 동안은 텍스트 출력 X
    public static bool isFinished = false;   // 페이드 완료 여부


    public IEnumerator FadeOut(bool _isWhite, bool _isSlow)
    {
        Color t_color = (_isWhite == true) ? colorWhite : colorBlack;   // 배경색 결정

        t_color.a = 0;
        img.color = t_color;

        while (t_color.a < 1)   // 점점 불투명하게
        {
            t_color.a += (_isSlow == true) ? fadeSlowSpeed : fadeSpeed; // 페이드아웃 속도 결정
            img.color = t_color;

            yield return null;
        }
        
        isFinished = true;  // 페이드아웃 완료
    }

    public IEnumerator FadeIn(bool _isWhite, bool _isSlow)
    {
        Color t_color = (_isWhite == true) ? colorWhite : colorBlack;

        t_color.a = 1;
        img.color = t_color;

        while (t_color.a > 0)   // 점점 투명하게
        {
            t_color.a -= (_isSlow == true) ? fadeSlowSpeed : fadeSpeed;
            img.color = t_color;

            yield return null;
        }

        isFinished = true;
    }
}
