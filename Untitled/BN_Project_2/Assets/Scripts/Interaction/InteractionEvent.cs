using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] bool isAutoEvent = false;  // true �� �ڵ����� ����Ǵ� �̺�Ʈ

    [SerializeField] DialogueEvent dialogueEvent;

    DialogueManager theDM;

    // �� �̺�Ʈ(ĳ����)�� �����ų�� ���� �����ϴ� �Լ�
    // �̺�Ʈ ���� �� ĳ���͸� �����Ű�� �ڵ�� Update �Լ��� �ۼ�
    bool CheckEvent()
    {
        bool t_flag = true; // true�� ����, false�� ���� X

        for (int i = 0; i < dialogueEvent.eventTiming.eventConditions.Length; i++)
        {
            // 1. ���� ���ǰ� �� �̺�Ʈ�� ��ġ���� ������, ĳ���� ���� X
            // ���� �̺�Ʈ�� ����Ǿ������� ���� (true, false)
            // �ش� �̺�Ʈ�� ���ƾ� �����ϴ����� ���� (true, false)
            if (DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventConditions[i]] != dialogueEvent.eventTiming.conditionFlag)
            {
                t_flag = false;
                break;
            }

            // 2. ĳ���� ���� ������ �Ǵ� �̺�Ʈ�� ���Ҵٸ�, ĳ���� ���� X
            if (DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventEndNum])
            {
                t_flag = false; // ĳ���� ���� X
            }
        }

        return t_flag;
    }

    // DatabaseManager�� ����� ���� ��� �����͸� �����´�.
    public Dialogue[] GetDialogue()
    {
        // ��ȣ�ۿ� �� ��ȭ
        if (!DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventNum])
        {
            DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventNum] = true; // ���� �̺�Ʈ�� ���� ���θ� true�� ����
            dialogueEvent.dialogues = SetDialogue(dialogueEvent.dialogues, (int)dialogueEvent.line.x, (int)dialogueEvent.line.y);

            return dialogueEvent.dialogues;
        }

        // ��ȣ�ۿ� ���� ��ȭ (���� ���)
        else if (dialogueEvent.dialoguesAfter.Length > 0)
        {
            dialogueEvent.dialoguesAfter = SetDialogue(dialogueEvent.dialoguesAfter, (int)dialogueEvent.lineAfter.x, (int)dialogueEvent.lineAfter.y);

            return dialogueEvent.dialoguesAfter;
        }

        // ��ȣ�ۿ� ���� ��ȭ�� ���� ���, ó�� ��ȭ�� ��� ����Ѵ�.
        // �ٵ� �� ���� ���� �� ���� �ϴ�.
        return dialogueEvent.dialogues;
    }

    // ��ȣ�ۿ� ���� ��縦 ���� (p_dialogue: ��ȣ�ۿ� �� �Ǵ� �� ���)
    Dialogue[] SetDialogue(Dialogue[] p_dialogue, int p_lineX, int p_lineY)
    {
        Dialogue[] t_dialogues = DatabaseManager.instance.GetDialogue(p_lineX, p_lineY);

        for (int i = 0; i < t_dialogues.Length; i++)
        {
            // dialogueEvent�� ���� Standing Image ������Ʈ�� �ӽ� ������ �ֱ�
            // t_dialogues[i].tf_standing = p_dialogue[i].tf_standing;

            // dialogueEvent�� ���� ī�޶� Ÿ���� �ӽ� ������ �ֱ�
            t_dialogues[i].cameraType = p_dialogue[i].cameraType;
        }

        return t_dialogues;
    }

    public AppearType GetAppearType()
    {
        return dialogueEvent.appearType;
    }

    public GameObject[] GetAppearTargets()
    {
        return dialogueEvent.go_appearTargets;
    }

    public GameObject[] GetDisppearTargets()
    {
        return dialogueEvent.go_disappearTargets;
    }

    public GameObject GetNextEvent()
    {
        return dialogueEvent.go_nextEvent;
    }

    // �ش� �̺�Ʈ�� ��ȣ�� �������� �Լ�
    public int GetEventNum()
    {
        return dialogueEvent.eventTiming.eventNum;
    }

    private void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theDM.SetNextEvent(GetNextEvent()); // ���� �̺�Ʈ ������ ����

        bool t_flag = CheckEvent(); // �� �̺�Ʈ(ĳ����)�� �����ų��
        gameObject.SetActive(t_flag);
    }

    private void Update()
    {
        // �ڵ� �̺�Ʈ�̰�, ������ �Ľ� �� ���̺� ��� ����Ǹ� (���� ����)
        // + �̵��� ��� �����ٸ� (�̵��� ���ϴ� ��쵵 �⺻���� true�̹Ƿ� �ڵ� �̺�Ʈ�� ������ ���ΰ� �����)
        if (isAutoEvent && DatabaseManager.isFinish && TransferManager.isFinished)
        {
            DialogueManager.isWaiting = true;   // true �� �ڵ� �̺�Ʈ ���

            // ������Ʈ ����/����
            if (GetAppearType() == AppearType.Change)
            {
                theDM.SetAppearObjects(GetAppearTargets(), GetDisppearTargets());
            }

            theDM.ShowDialogue(GetDialogue());

            gameObject.SetActive(false);    // �ڵ� �̺�Ʈ�� �� ���� ���� ���� �ƿ� ��Ȱ��ȭ�Ѵ�.
        }
    }
}
