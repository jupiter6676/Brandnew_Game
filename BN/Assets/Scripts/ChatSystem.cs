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
    public GameObject quad;
    
    public void OnDialogue(string[] lines, Transform chatPoint)
    {
        // ChatBox의 위치를, chatPoint의 위치로 초기화
        transform.position = chatPoint.position;
        
        // 큐 초기화
        sentences = new Queue<string>();
        sentences.Clear();

        foreach(var line in lines)
        {
            sentences.Enqueue(line);    // string 배열의 값들을 차례로 큐에 넣기
        }

        StartCoroutine(DialogueFlow(chatPoint));
    }

    // 큐에 담은 string을 코루틴을 이용해 차례로 말풍선으로 띄우기
    IEnumerator DialogueFlow(Transform chatPoint)
    {
        yield return null;

        while (sentences.Count > 0)
        {
            currentSentence = sentences.Dequeue();

            
            // TextMeshPro의 text에 대사 담기
            text.text = currentSentence;

            // TextMesh에 대사를 넣은 후, 크기에 맞게 Quad의 크기를 변화
            float x = text.preferredWidth;

            // 1. x의 길이가 3보다 크면 3을, 작으면 x + 0.3f를 반환
            x = (x > 3) ? 3 : x + 0.3f;
            
            // 2. Quad의 Scale 초기화
            quad.transform.localScale = new Vector2(x, text.preferredHeight + 0.3f);

            // 3. 말풍선의 크기가 초기화된 후, 크기에 맞춰 다시 위치 초기화
            transform.position = new Vector2(chatPoint.position.x, chatPoint.position.y + text.preferredHeight / 2);

            // 4. 대사를 한 줄 담았으니, 조금 후에 다음 대사 담기
            yield return new WaitForSeconds(3f);
        }

        Destroy(gameObject);    // 큐의 대사를 모두 말했으면, 말풍선 파괴
    }

    void Start()
    {
        
    }

    void Update()
    {
       
    }
}
