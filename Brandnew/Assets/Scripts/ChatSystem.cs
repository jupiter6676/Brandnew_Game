using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    // String�� ���� Queue ����
    public Queue<string> sentences;
    public string currentSentence;
    public TextMeshPro text;
    
    public void OnDialogue(string[] lines)
    {
        // ť �ʱ�ȭ
        sentences = new Queue<string>();
        sentences.Clear();

        foreach(var line in lines)
        {
            sentences.Enqueue(line);    // string �迭�� ������ ���ʷ� ť�� �ֱ�
        }

        // ť�� ���� string�� �ڷ�ƾ�� �̿��� ���ʷ� ��ǳ������ ����
        IEnumerator DialogueFlow()
        {
            yield return null;

            while (sentences.Count > 0)
            {
                currentSentence = sentences.Dequeue();

                // TextMeshPro�� text�� ��� ���
                text.text = currentSentence;

                // ��縦 �� �� �������, ���� �Ŀ� ���� ��� ���

                yield return new WaitForSeconds(3f);
            }

            Destroy(gameObject);    // ť�� ��縦 ��� ��������, ��ǳ�� �ı�
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
       
    }
}
