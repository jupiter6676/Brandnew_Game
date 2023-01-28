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
        List<Dialogue> dialogueList = new List<Dialogue>(); // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);    // TextAsset 형태로 CSV를 가져와서 그 데이터를 담는다.

        string[] data = csvData.text.Split(new char[] { '\n' });    // 엔터 단위로 대사 쪼개기
        
        for (int i = 1; i < data.Length;)   // data[0] = {'ID', '캐릭터 이름', '대사'}
        {
            string[] row = data[i].Split(new char[] { ',' });   // , 단위로 쪼개기

            Dialogue dialogue = new Dialogue(); // 캐릭터 한 명의 대사들

            dialogue.name = row[1];
            Debug.Log(row[1]);

            List<string> contextList = new List<string>();

            do
            {
                contextList.Add(row[2]);
                Debug.Log(row[2]);

                // 다음 줄 미리 비교
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }

                // 다음 줄이 데이터 보다 길어지면 그냥 break
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");  // 다음 줄 캐릭터 이름이 공백이면 대사를 더 채우기

            dialogue.contexts = contextList.ToArray();   // 리스트를 배열로
            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();
    }
}
