using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    //private void Start()
    //{
    //    Parse("dialogue_test_1");
    //}

    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); // ��� ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);    // TextAsset ���·� CSV�� �����ͼ� �� �����͸� ��´�.

        string[] data = csvData.text.Split(new char[] { '\n' });    // ���� ������ ��� �ɰ���
        
        for (int i = 1; i < data.Length;)   // data[0] = {'ID', 'ĳ���� �̸�', '���'}
        {
            string[] row = data[i].Split(new char[] { ',' });   // , ������ �ɰ���

            Dialogue dialogue = new Dialogue(); // ĳ���� �� ���� ����

            dialogue.name = row[1];
            List<string> contextList = new List<string>();  // ��� ����Ʈ
            List<string> spriteList = new List<string>();   // ���ĵ� �̹��� ����Ʈ
            List<string> cutsceneList = new List<string>(); // �ƽ� �̹��� ����Ʈ

            do
            {
                contextList.Add(row[2]);
                spriteList.Add(row[3]);
                cutsceneList.Add(row[4]);

                // ���� �� �̸� ��
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }

                // ���� ���� ������ ���� ������� �׳� break
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");  // ���� �� ĳ���� �̸��� �����̸� ��縦 �� ä���

            dialogue.contexts = contextList.ToArray();   // ����Ʈ�� �迭��
            dialogue.spriteName = spriteList.ToArray();
            dialogue.cutsceneName = cutsceneList.ToArray();
            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();
    }
}
