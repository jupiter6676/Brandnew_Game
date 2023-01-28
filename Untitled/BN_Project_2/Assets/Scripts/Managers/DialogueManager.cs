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

    Dialogue[] dialogues;

    bool isDialogue = false;    // ��ȭ���� ��� true
    bool isNext = false;    // Ư�� Ű �Է� ��⸦ ���� ����. true�� �� ����, �����̽� Ű �Է� O

    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;

    int dialogueCnt = 0;    // ��ȭ ī��Ʈ. �� ĳ���Ͱ� �� ���ϸ� 1 ����
    int contextCnt = 0;     // ��� ī��Ʈ. �� ĳ���Ͱ� ���� ��縦 �� �� �ִ�.

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
                            StartCoroutine(TypeWriter());
                        }

                        // ���� ĳ���Ͱ� ������ (��ȭ�� ��������)
                        else
                        {
                            EndDialogue();
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

        ic.SettingUI(false);    // Ŀ��, ����â �����
        SettingUI(true);    // ���â, �̸�â ���̱�

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

        ic.SettingUI(true); // Ŀ��, ����â ���̱�
        SettingUI(false);   // ���â, �̸�â �����
    }

    // �ؽ�Ʈ ��� �ڷ�ƾ
    IEnumerator TypeWriter()
    {
        SettingUI(true);    // ���â �̹����� ����.

        string t_ReplaceText = dialogues[dialogueCnt].contexts[contextCnt];   // Ư�����ڸ� ,�� ġȯ
        t_ReplaceText = t_ReplaceText.Replace("`", ",");    // backtick�� comma�� ��ȯ

        txt_name.text = dialogues[dialogueCnt].name;
        
        // �� ���ھ� ���
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            txt_dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }
        
        isNext = true; // ���� ��縦 ��� �����ϵ���
    }

    // ���â Ȱ��ȭ
    void SettingUI(bool p_flag)
    {
        go_dialogueBar.SetActive(p_flag);
        go_NameBar.SetActive(p_flag);
    }
}
