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
    public GameObject quad;
    
    public void OnDialogue(string[] lines, Transform chatPoint)
    {
        // ChatBox�� ��ġ��, chatPoint�� ��ġ�� �ʱ�ȭ
        transform.position = chatPoint.position;
        
        // ť �ʱ�ȭ
        sentences = new Queue<string>();
        sentences.Clear();

        foreach(var line in lines)
        {
            sentences.Enqueue(line);    // string �迭�� ������ ���ʷ� ť�� �ֱ�
        }

        StartCoroutine(DialogueFlow(chatPoint));
    }

    // ť�� ���� string�� �ڷ�ƾ�� �̿��� ���ʷ� ��ǳ������ ����
    IEnumerator DialogueFlow(Transform chatPoint)
    {
        yield return null;

        while (sentences.Count > 0)
        {
            currentSentence = sentences.Dequeue();

            
            // TextMeshPro�� text�� ��� ���
            text.text = currentSentence;

            // TextMesh�� ��縦 ���� ��, ũ�⿡ �°� Quad�� ũ�⸦ ��ȭ
            float x = text.preferredWidth;

            // 1. x�� ���̰� 3���� ũ�� 3��, ������ x + 0.3f�� ��ȯ
            x = (x > 3) ? 3 : x + 0.3f;
            
            // 2. Quad�� Scale �ʱ�ȭ
            quad.transform.localScale = new Vector2(x, text.preferredHeight + 0.3f);

            // 3. ��ǳ���� ũ�Ⱑ �ʱ�ȭ�� ��, ũ�⿡ ���� �ٽ� ��ġ �ʱ�ȭ
            transform.position = new Vector2(chatPoint.position.x, chatPoint.position.y + text.preferredHeight / 2);

            // 4. ��縦 �� �� �������, ���� �Ŀ� ���� ��� ���
            yield return new WaitForSeconds(3f);
        }

        Destroy(gameObject);    // ť�� ��縦 ��� ��������, ��ǳ�� �ı�
    }

    void Start()
    {
        
    }

    void Update()
    {
       
    }
}
