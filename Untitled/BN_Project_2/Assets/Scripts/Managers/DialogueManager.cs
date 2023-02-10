using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;   // TextMeshProUGUI, TMP_Text 클래스 사용

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_standingImage;
    [SerializeField] GameObject go_dialogueBar;
    [SerializeField] GameObject go_nameBar;

    [SerializeField] TMP_Text txt_dialogue;
    [SerializeField] TMP_Text txt_name;

    Dialogue[] dialogues;

    bool isDialogue = false;    // 대화중인 경우 true
    bool isNext = false;    // 특정 키 입력 대기를 위한 변수. true일 시 엔터, 스페이스 키 입력 O

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int dialogueCnt = 0;    // 대화 카운트. 한 캐릭터가 다 말하면 1 증가
    int contextCnt = 0;     // 대사 카운트. 한 캐릭터가 여러 대사를 할 수 있다.

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

                    // 현재 캐릭터의 다음 대사 출력
                    if (++contextCnt < dialogues[dialogueCnt].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }

                    // 다음 캐릭터의 대사 출력
                    else
                    {
                        contextCnt = 0;

                        if (++dialogueCnt < dialogues.Length)
                        {
                            // cam.CameraTragetting(dialogues[dialogueCnt].tf_target);
                            // StartCoroutine(TypeWriter());
                            StartCoroutine(CameraTargettingType());
                        }

                        // 다음 캐릭터가 없으면 (대화가 끝났으면)
                        else
                        {
                            StartCoroutine(EndDialogue());
                        }
                    }
                }
            }
        }
    }

    // 다른 곳에서 호출 가능하도록, public으로 생성
    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_dialogue.text = "";
        txt_name.text = "";

        theInteractionController.SettingUI(false);    // 커서, 상태창 숨기기

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
                StartCoroutine(theSplashManager.FadeIn(false, true));  // 검은 화면, 느리게 전환
                yield return new WaitUntil(() => SplashManager.isFinished); // isFinished가 true가 될 때까지 대기
                break;

            case CameraType.FadeOut:
                SettingUI(false);
                SplashManager.isFinished = false;
                StartCoroutine(theSplashManager.FadeOut(false, true));  // 흰 화면, 느리게 전환
                yield return new WaitUntil(() => SplashManager.isFinished); // isFinished가 true가 될 때까지 대기
                break;

            case CameraType.FlashIn:
                SettingUI(false);
                SplashManager.isFinished = false;
                StartCoroutine(theSplashManager.FadeIn(true, true));  // 검은 화면, 느리게 전환
                yield return new WaitUntil(() => SplashManager.isFinished); // isFinished가 true가 될 때까지 대기
                break;

            case CameraType.FlashOut:
                SettingUI(false);
                SplashManager.isFinished = false;
                StartCoroutine(theSplashManager.FadeOut(true, true));  // 흰 화면, 느리게 전환
                yield return new WaitUntil(() => SplashManager.isFinished); // isFinished가 true가 될 때까지 대기
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

        theInteractionController.SettingUI(true); // 커서, 상태창 보이기
        SettingUI(false);   // 대사창, 이름창 숨기기
    }

    void ChangeSprite()
    {
        // 캐릭터가 대사를 할 때, spriteName이 공백이 아니면 이미지 변경
        //if (dialogues[dialogueCnt].spriteName[contextCnt] != "")
        //{
        //    StartCoroutine(sm.SpriteChangeCoroutine(dialogues[dialogueCnt].tf_standing, dialogues[dialogueCnt].spriteName[contextCnt]));
        //}

        StartCoroutine(theSpriteManager.SpriteChangeCoroutine(dialogues[dialogueCnt].tf_standing, dialogues[dialogueCnt].spriteName[contextCnt]));
    }

    // 텍스트 출력 코루틴
    IEnumerator TypeWriter()
    {
        SettingUI(true);    // 대사창 이미지를 띄운다.
        ChangeSprite();     // 스탠딩 이미지를 변경한다.

        string t_ReplaceText = dialogues[dialogueCnt].contexts[contextCnt];
        t_ReplaceText = t_ReplaceText.Replace("`", ",");    // backtick을 comma로 변환
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n"); // 엑셀의 \n은 텍스트이기 때문에, 앞에 \를 한 번 더 입력

        bool t_white = false, t_red = false, t_gray = false;    // 글자색
        bool t_ignore = false;  // 특수문자는 대사로 출력 X
        
        // 한 글자씩 출력
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch (t_ReplaceText[i])
            {
                case 'ⓦ': 
                    t_white = true; t_red = false; t_gray = false; t_ignore = true;
                    break;

                case 'ⓡ':
                    t_white = false; t_red = true; t_gray = false; t_ignore = true;
                    break;

                case 'ⓖ':
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

                txt_dialogue.text += t_letter;  // 특수문자가 아니면 대사 출력
            }
            t_ignore = false;   // 한 글자를 찍으면 다시 false로

            yield return new WaitForSeconds(textDelay);
        }
        
        isNext = true; // 다음 대사를 출력 가능하도록
    }

    // 대사창 활성화
    void SettingUI(bool p_flag)
    {
        go_dialogueBar.SetActive(p_flag);
        go_standingImage.SetActive(p_flag);

        if (p_flag)
        {
            // 독백이면 캐릭터 이름창 표시 X
            if (dialogues[dialogueCnt].name == "")
            {
                go_nameBar.SetActive(false);
            }

            // 독백이 아닌 경우 캐릭터 이름창 표시 O
            else
            {
                go_nameBar.SetActive(true);
                txt_name.text = dialogues[dialogueCnt].name;    // 캐릭터 이름
            }
        }

        else
        {
            go_nameBar.SetActive(false);
        }
    }
}
