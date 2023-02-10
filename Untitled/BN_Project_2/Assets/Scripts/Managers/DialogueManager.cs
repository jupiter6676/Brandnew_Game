using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;   // TextMeshProUGUI, TMP_Text Ŭ���� ���

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_standingImage;
    [SerializeField] GameObject go_dialogueBar;
    [SerializeField] GameObject go_nameBar;

    [SerializeField] TMP_Text txt_dialogue;
    [SerializeField] TMP_Text txt_name;

    Dialogue[] dialogues;

    bool isDialogue = false;    // ��ȭ���� ��� true
    bool isNext = false;    // Ư�� Ű �Է� ��⸦ ���� ����. true�� �� ����, �����̽� Ű �Է� O

    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;

    int dialogueCnt = 0;    // ��ȭ ī��Ʈ. �� ĳ���Ͱ� �� ���ϸ� 1 ����
    int contextCnt = 0;     // ��� ī��Ʈ. �� ĳ���Ͱ� ���� ��縦 �� �� �ִ�.

    InteractionController theInteractionController;
    SpriteManager theSpriteManager;
    SplashManager theSplashManager;
    CutsceneManager theCutsceneManager;


    void Start()
    {
        theInteractionController = FindObjectOfType<InteractionController>();
        theSpriteManager = FindObjectOfType<SpriteManager>();
        theSplashManager = FindObjectOfType<SplashManager>();
        theCutsceneManager = FindObjectOfType<CutsceneManager>();
    }

    private void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isNext = false;
                    txt_dialogue.text = "";

                    // ���� ĳ������ ���� ��� ���
                    if (++contextCnt < dialogues[dialogueCnt].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }

                    // ���� ĳ������ ��� ���
                    else
                    {
                        contextCnt = 0;

                        if (++dialogueCnt < dialogues.Length)
                        {
                            // cam.CameraTragetting(dialogues[dialogueCnt].tf_target);
                            // StartCoroutine(TypeWriter());
                            StartCoroutine(CameraTargettingType());
                        }

                        // ���� ĳ���Ͱ� ������ (��ȭ�� ��������)
                        else
                        {
                            StartCoroutine(EndDialogue());
                        }
                    }
                }
            }
        }
    }

    // �ٸ� ������ ȣ�� �����ϵ���, public���� ����
    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_dialogue.text = "";
        txt_name.text = "";

        theInteractionController.SettingUI(false);    // Ŀ��, ����â �����

        dialogues = p_dialogues;

        // cam.CameraTragetting(dialogues[dialogueCnt].tf_target);
        // StartCoroutine(TypeWriter());
        StartCoroutine(CameraTargettingType());
    }

    IEnumerator CameraTargettingType()
    {
        switch (dialogues[dialogueCnt].cameraType)
        {
            case CameraType.FadeIn:
                SettingUI(false);
                SplashManager.isFinished = false;
                StartCoroutine(theSplashManager.FadeIn(false, true));  // ���� ȭ��, ������ ��ȯ
                yield return new WaitUntil(() => SplashManager.isFinished); // isFinished�� true�� �� ������ ���
                break;

            case CameraType.FadeOut:
                SettingUI(false);
                SplashManager.isFinished = false;
                StartCoroutine(theSplashManager.FadeOut(false, true));  // �� ȭ��, ������ ��ȯ
                yield return new WaitUntil(() => SplashManager.isFinished); // isFinished�� true�� �� ������ ���
                break;

            case CameraType.FlashIn:
                SettingUI(false);
                SplashManager.isFinished = false;
                StartCoroutine(theSplashManager.FadeIn(true, true));  // ���� ȭ��, ������ ��ȯ
                yield return new WaitUntil(() => SplashManager.isFinished); // isFinished�� true�� �� ������ ���
                break;

            case CameraType.FlashOut:
                SettingUI(false);
                SplashManager.isFinished = false;
                StartCoroutine(theSplashManager.FadeOut(true, true));  // �� ȭ��, ������ ��ȯ
                yield return new WaitUntil(() => SplashManager.isFinished); // isFinished�� true�� �� ������ ���
                break;

            case CameraType.ShowCutscene:
                SettingUI(false);
                CutsceneManager.isFinished = false;
                StartCoroutine(theCutsceneManager.CutsceneCoroutine(dialogues[dialogueCnt].cutsceneName[contextCnt], true));
                yield return new WaitUntil(() => CutsceneManager.isFinished);
                break;

            case CameraType.HideCutscene:
                SettingUI(false);
                CutsceneManager.isFinished = false;
                StartCoroutine(theCutsceneManager.CutsceneCoroutine(null, false));
                yield return new WaitUntil(() => CutsceneManager.isFinished);
                break;
        }

        StartCoroutine(TypeWriter());
    }

    IEnumerator EndDialogue()
    {
        // Hide Cutscene
        if (theCutsceneManager.CheckCutscene())
        {
            SettingUI(false);
            CutsceneManager.isFinished = false;
            StartCoroutine(theCutsceneManager.CutsceneCoroutine(null, false));
            yield return new WaitUntil(() => CutsceneManager.isFinished);
        }

        isDialogue = false;
        contextCnt = 0;
        dialogueCnt = 0;
        dialogues = null;
        isNext = false;

        theInteractionController.SettingUI(true); // Ŀ��, ����â ���̱�
        SettingUI(false);   // ���â, �̸�â �����
    }

    void ChangeSprite()
    {
        // ĳ���Ͱ� ��縦 �� ��, spriteName�� ������ �ƴϸ� �̹��� ����
        //if (dialogues[dialogueCnt].spriteName[contextCnt] != "")
        //{
        //    StartCoroutine(sm.SpriteChangeCoroutine(dialogues[dialogueCnt].tf_standing, dialogues[dialogueCnt].spriteName[contextCnt]));
        //}

        StartCoroutine(theSpriteManager.SpriteChangeCoroutine(dialogues[dialogueCnt].tf_standing, dialogues[dialogueCnt].spriteName[contextCnt]));
    }

    // �ؽ�Ʈ ��� �ڷ�ƾ
    IEnumerator TypeWriter()
    {
        SettingUI(true);    // ���â �̹����� ����.
        ChangeSprite();     // ���ĵ� �̹����� �����Ѵ�.

        string t_ReplaceText = dialogues[dialogueCnt].contexts[contextCnt];
        t_ReplaceText = t_ReplaceText.Replace("`", ",");    // backtick�� comma�� ��ȯ
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n"); // ������ \n�� �ؽ�Ʈ�̱� ������, �տ� \�� �� �� �� �Է�

        bool t_white = false, t_red = false, t_gray = false;    // ���ڻ�
        bool t_ignore = false;  // Ư�����ڴ� ���� ��� X
        
        // �� ���ھ� ���
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch (t_ReplaceText[i])
            {
                case '��': 
                    t_white = true; t_red = false; t_gray = false; t_ignore = true;
                    break;

                case '��':
                    t_white = false; t_red = true; t_gray = false; t_ignore = true;
                    break;

                case '��':
                    t_white = false; t_red = false; t_gray = true; t_ignore = true;
                    break;
            }

            string t_letter = t_ReplaceText[i].ToString();
            if (!t_ignore)
            {
                if (t_white)
                {
                    t_letter = "<color=#ffffff>" + t_letter + "</color>";    // HTML Tag
                }

                else if (t_red)
                {
                    t_letter = "<color=#E33131>" + t_letter + "</color>";
                }

                else if (t_gray)
                {
                    t_letter = "<color=#A2A2A2>" + t_letter + "</color>";
                }

                txt_dialogue.text += t_letter;  // Ư�����ڰ� �ƴϸ� ��� ���
            }
            t_ignore = false;   // �� ���ڸ� ������ �ٽ� false��

            yield return new WaitForSeconds(textDelay);
        }
        
        isNext = true; // ���� ��縦 ��� �����ϵ���
    }

    // ���â Ȱ��ȭ
    void SettingUI(bool p_flag)
    {
        go_dialogueBar.SetActive(p_flag);
        go_standingImage.SetActive(p_flag);

        if (p_flag)
        {
            // �����̸� ĳ���� �̸�â ǥ�� X
            if (dialogues[dialogueCnt].name == "")
            {
                go_nameBar.SetActive(false);
            }

            // ������ �ƴ� ��� ĳ���� �̸�â ǥ�� O
            else
            {
                go_nameBar.SetActive(true);
                txt_name.text = dialogues[dialogueCnt].name;    // ĳ���� �̸�
            }
        }

        else
        {
            go_nameBar.SetActive(false);
        }
    }
}
