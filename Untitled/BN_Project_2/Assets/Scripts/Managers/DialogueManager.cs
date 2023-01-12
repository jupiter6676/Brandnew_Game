using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;   // TextMeshProUGUI, TMP_Text Ŭ���� ���

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_dialogueBar;
    [SerializeField] GameObject go_NameBar;

    [SerializeField] TMP_Text txt_dialogue;
    [SerializeField] TMP_Text txt_name;

    bool isDialogue = false;    // ���� ��ȭ������

    InteractionController ic;


    void Start()
    {
        ic = FindObjectOfType<InteractionController>();
    }

    // �ٸ� ������ ȣ�� �����ϵ���, public���� ����
    public void ShowDialogue()
    {
        txt_dialogue.text = "";
        txt_name.text = "";

        ic.HideUI();    // Ŀ��, ����â �����
        SettingUI(true);    // ���â, �̸�â ���̱�
    }

    // ���â Ȱ��ȭ
    void SettingUI(bool p_flag)
    {
        go_dialogueBar.SetActive(p_flag);
        go_NameBar.SetActive(p_flag);
    }
}
