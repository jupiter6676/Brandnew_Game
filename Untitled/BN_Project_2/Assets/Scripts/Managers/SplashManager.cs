using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [SerializeField] Image img; // ��� �̹��� �� �긦 ���̵���/�ƿ�

    [SerializeField] Color colorWhite;
    [SerializeField] Color colorBlack;

    [SerializeField] float fadeSpeed;
    [SerializeField] float fadeSlowSpeed;

    // ���̵�Ǵ� ������ �ؽ�Ʈ ��� X
    public static bool isFinished = false;   // ���̵� �Ϸ� ����


    public IEnumerator FadeOut(bool _isWhite, bool _isSlow)
    {
        Color t_color = (_isWhite == true) ? colorWhite : colorBlack;   // ���� ����

        t_color.a = 0;
        img.color = t_color;

        while (t_color.a < 1)   // ���� �������ϰ�
        {
            t_color.a += (_isSlow == true) ? fadeSlowSpeed : fadeSpeed; // ���̵�ƿ� �ӵ� ����
            img.color = t_color;

            yield return null;
        }
        
        isFinished = true;  // ���̵�ƿ� �Ϸ�
    }

    public IEnumerator FadeIn(bool _isWhite, bool _isSlow)
    {
        Color t_color = (_isWhite == true) ? colorWhite : colorBlack;

        t_color.a = 1;
        img.color = t_color;

        while (t_color.a > 0)   // ���� �����ϰ�
        {
            t_color.a -= (_isSlow == true) ? fadeSlowSpeed : fadeSpeed;
            img.color = t_color;

            yield return null;
        }

        isFinished = true;
    }
}
