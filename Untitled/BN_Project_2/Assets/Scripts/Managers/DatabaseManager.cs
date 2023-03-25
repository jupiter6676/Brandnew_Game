using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance; // DB 자체를 인스턴스화, 참조가 편하게 static

    [SerializeField] string csv_fileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();   // int로 대사를 찾는다.

    public bool[] eventFlags = new bool[100];   // i번 이벤트가 실행되면, 그 값을 true로 변경 (InteractionEvent에서)

    public static bool isFinish = false;    // 파싱한 데이터를 모두 저장했는지

    // Start보다 먼저 실행되는 함수
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    // 자기 자신을 넣어준다.
            DialogueParser parser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = parser.Parse(csv_fileName);  // dialogues에 모든 데이터가 담기게 된다.

            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]);    // 키는 1부터 시작
            }

            isFinish = true;
        }
    }


    // _StartNum ~ _EndNum 사이의 대사를 가지고 옴.
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
