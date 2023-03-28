using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public static bool isFinished = false;  // �ƽ��� �� �ҷ���������

    SplashManager theSplashManager;

    [SerializeField] Image img_cutscene;


    void Start()
    {
        theSplashManager = FindObjectOfType<SplashManager>();
    }

    public bool CheckCutscene()
    {
        return img_cutscene.gameObject.activeSelf;  // ���� �� ��ü�� Ȱ��ȭ �������� ��ȯ
    }

    public IEnumerator CutsceneCoroutine(string p_cutsceneName, bool p_isShow)
    {
        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeOut(true, false));      // �Ͼ�� ������
        yield return new WaitUntil(() => SplashManager.isFinished); // ���̵�ƿ� ���� ������ ���

        // �ƽ��� �����ش�.
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

        // �ƾ��� �������� �ʴ´�.
        else
        {
            img_cutscene.gameObject.SetActive(false);
        }

        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeIn(true, false));       // �Ͼ�� ������
        yield return new WaitUntil(() => SplashManager.isFinished); // ���̵��� ���� ������ ���

        yield return new WaitForSeconds(0.5f);  // �ƽ� ������ 0.5�� �� �ؽ�Ʈ ���

        isFinished = true;  // �ƽ� �ҷ����� ��
    }
}
