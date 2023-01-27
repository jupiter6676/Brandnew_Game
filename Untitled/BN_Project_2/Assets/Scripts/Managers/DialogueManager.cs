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

    bool isDialogue = false;    // 현재 대화중인지

    InteractionController ic;


    void Start()
    {
        ic = FindObjectOfType<InteractionController>();
    }

    // 다른 곳에서 호출 가능하도록, public으로 생성
    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        txt_dialogue.text = "";
        txt_name.text = "";

        dialogues = p_dialogues;

        ic.HideUI();    // 커서, 상태창 숨기기
        SettingUI(true);    // 대사창, 이름창 보이기
    }

    // 대사창 활성화
    void SettingUI(bool p_flag)
    {
        go_dialogueBar.SetActive(p_flag);
        go_NameBar.SetActive(p_flag);
    }
}
