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
        SceneManager.sceneLoaded += LoadSceneEvent; // �̺�Ʈ ���
    }

    // ���� �ٲ� ������ ȣ��
    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (activityDict.Count > 0)
        {
            GameObject parent = GameObject.Find("Objects");     // Objects ������ ��ȣ�ۿ� ������Ʈ �α� (in Hierachy)
            
            foreach (KeyValuePair<string, bool> dict in activityDict)
            {
                Transform child = parent.transform.Find(dict.Key);  // �ڽ� ������Ʈ ã�� (��Ȱ�� ��ü�� ã�� �� �ִ�.)

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

        // ��ųʸ� ���
        //foreach (KeyValuePair<string, bool> dict in activityDict)
        //{
        //    Debug.Log(dict.Key + " " + dict.Value);
        //}
    }

    void SaveActivity()
    {
        // �̺�Ʈ�� ���ƾ� go_appearTargets�� go_disappearTargets�� ������Ʈ�� ä������.
        // �̺�Ʈ�� �� ��, ������Ʈ �̸��� ����(appear = True, disappear = False)�� �������ش�.

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
