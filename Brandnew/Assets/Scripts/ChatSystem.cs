using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    // String을 담을 Queue 선언
    public Queue<string> sentences;
    public string currentSentence;
    public TextMeshPro text;
    
    public void OnDialogue(string[] lines)
    {
        // 큐 초기화
        sentences = new Queue<string>();
        sentences.Clear();

        foreach(var line in lines)
        {
            sentences.Enqueue(line);    // string 배열의 값들을 차례로 큐에 넣기
        }

        // 큐에 담은 string을 코루틴을 이용해 차례로 말풍선으로 띄우기
        IEnumerator DialogueFlow()
        {
            yield return null;

            while (sentences.Count > 0)
            {
                currentSentence = sentences.Dequeue();

                // TextMeshPro의 text에 대사 담기
                text.text = currentSentence;

                // 대사를 한 줄 담았으니, 조금 후에 다음 대사 담기

                yield return new WaitForSeconds(3f);
            }

            Destroy(gameObject);    // 큐의 대사를 모두 말했으면, 말풍선 파괴
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
       
    }
}
