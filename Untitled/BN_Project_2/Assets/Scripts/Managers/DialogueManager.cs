using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;   // TextMeshProUGUI, TMP_Text 클래스 사용

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_dialogueBar;
    [SerializeField] GameObject go_NameBar;

    [SerializeField] TMP_Text txt_dialogue;
    [SerializeField] TMP_Text txt_name;

    Dialogue[] dialogues;

    bool isDialogue = false;    // 대화중인 경우 true
    bool isNext = false;    // 특정 키 입력 대기를 위한 변수. true일 시 엔터, 스페이스 키 입력 O

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int dialogueCnt = 0;    // 대화 카운트. 한 캐릭터가 다 말하면 1 증가
    int contextCnt = 0;     // 대사 카운트. 한 캐릭터가 여러 대사를 할 수 있다.

    InteractionController ic;


    void Start()
    {
        ic = FindObjectOfType<InteractionController>();
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
                            StartCoroutine(TypeWriter());
                        }

                        // 다음 캐릭터가 없으면 (대화가 끝났으면)
                        else
                        {
                            EndDialogue();
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

        ic.SettingUI(false);    // 커서, 상태창 숨기기
        SettingUI(true);    // 대사창, 이름창 보이기

        dialogues = p_dialogues;

        StartCoroutine(TypeWriter());
    }

    void EndDialogue()
    {
        isDialogue = false;
        contextCnt = 0;
        dialogueCnt = 0;
        dialogues = null;
        isNext = false;

        ic.SettingUI(true); // 커서, 상태창 보이기
        SettingUI(false);   // 대사창, 이름창 숨기기
    }

    // 텍스트 출력 코루틴
    IEnumerator TypeWriter()
    {
        SettingUI(true);    // 대사창 이미지를 띄운다.

        string t_ReplaceText = dialogues[dialogueCnt].contexts[contextCnt];   // 특수문자를 ,로 치환
        t_ReplaceText = t_ReplaceText.Replace("`", ",");    // backtick을 comma로 변환

        txt_name.text = dialogues[dialogueCnt].name;
        
        // 한 글자씩 출력
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            txt_dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }
        
        isNext = true; // 다음 대사를 출력 가능하도록
    }

    // 대사창 활성화
    void SettingUI(bool p_flag)
    {
        go_dialogueBar.SetActive(p_flag);
        go_NameBar.SetActive(p_flag);
    }
}
