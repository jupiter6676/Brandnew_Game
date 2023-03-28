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
        DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventNum] = true; // ���� �̺�Ʈ�� ���� ���θ� true�� ����

        DialogueEvent t_dialogueEvent = new DialogueEvent();    // �ӽ� ����
        t_dialogueEvent.dialogues = DatabaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);

        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            // dialogueEvent�� ���� Standing Image ������Ʈ�� �ӽ� ������ �ֱ�
            // t_dialogueEvent.dialogues[i].tf_standing = dialogueEvent.dialogues[i].tf_standing;

            // dialogueEvent�� ���� ī�޶� Ÿ���� �ӽ� ������ �ֱ�
            t_dialogueEvent.dialogues[i].cameraType = dialogueEvent.dialogues[i].cameraType;
        }

        // ������ �ӽ� ���� �����
        dialogueEvent.dialogues = t_dialogueEvent.dialogues;

        return dialogueEvent.dialogues;
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
