using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivityManager : MonoBehaviour
{
    DialogueManager theDM;

    Dictionary<string, bool> activityDict = new Dictionary<string, bool>();

    private void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        SceneManager.sceneLoaded += LoadSceneEvent; // 이벤트 등록
    }

    // 신이 바뀔 때마다 호출
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (activityDict.Count > 0)
        {
            GameObject parent = GameObject.Find("Objects");     // Objects 하위에 상호작용 오브젝트 두기 (in Hierachy)
            
            foreach (KeyValuePair<string, bool> dict in activityDict)
            {
                Transform child = parent.transform.Find(dict.Key);  // 자식 오브젝트 찾기 (비활성 객체도 찾을 수 있다.)

                if (child != null)
                {
                    GameObject go = child.gameObject;
                    // Debug.Log(go.name);
                    go.SetActive(dict.Value);
                }
            }
        }
    }

    private void Update()
    {
        SaveActivity();

        // 딕셔너리 출력
        //foreach (KeyValuePair<string, bool> dict in activityDict)
        //{
        //    Debug.Log(dict.Key + " " + dict.Value);
        //}
    }

    void SaveActivity()
    {
        // 이벤트를 보아야 go_appearTargets나 go_disappearTargets에 오브젝트가 채워진다.
        // 이벤트를 본 후, 오브젝트 이름과 상태(appear = True, disappear = False)를 저장해준다.

        if (theDM.go_appearTargets != null)
        {
            for (int i = 0; i < theDM.go_appearTargets.Length; i++)
            {
                // TryAdd
                activityDict.TryAdd(theDM.go_appearTargets[i].name, true);
            }
        }
        
        if (theDM.go_disappearTargets != null)
        {
            for (int i = 0; i < theDM.go_disappearTargets.Length; i++)
            {
                activityDict.TryAdd(theDM.go_disappearTargets[i].name, false);
            }
        }
    }
}
