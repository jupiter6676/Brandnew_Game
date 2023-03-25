using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance; // DB ��ü�� �ν��Ͻ�ȭ, ������ ���ϰ� static

    [SerializeField] string csv_fileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();   // int�� ��縦 ã�´�.

    public bool[] eventFlags = new bool[100];   // i�� �̺�Ʈ�� ����Ǹ�, �� ���� true�� ���� (InteractionEvent����)

    public static bool isFinish = false;    // �Ľ��� �����͸� ��� �����ߴ���

    // Start���� ���� ����Ǵ� �Լ�
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    // �ڱ� �ڽ��� �־��ش�.
            DialogueParser parser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = parser.Parse(csv_fileName);  // dialogues�� ��� �����Ͱ� ���� �ȴ�.

            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]);    // Ű�� 1���� ����
            }

            isFinish = true;
        }
    }


    // _StartNum ~ _EndNum ������ ��縦 ������ ��.
    public Dialogue[] GetDialogue(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = _StartNum; i <= _EndNum; i++)
        {
            dialogueList.Add(dialogueDic[i]);
        }

        return dialogueList.ToArray();
    }
}
