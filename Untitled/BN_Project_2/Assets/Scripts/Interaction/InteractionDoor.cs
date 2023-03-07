using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : MonoBehaviour
{
    [SerializeField] string sceneName;      // ��Ҹ� �̵��� ��, ��� ������ �Ѿ�� ����
    [SerializeField] string locationName;   // ex) ��Ұ� �����̰� ���� ���� �� ���� ��, ������ ���� �ٸ� �̸��� �ִ�.

    public string GetSceneName()
    {
        return sceneName;
    }

    public string GetLocationName()
    {
        return locationName;
    }
}
