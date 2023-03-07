using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : MonoBehaviour
{
    [SerializeField] string sceneName;      // 장소를 이동할 때, 어느 신으로 넘어가게 할지
    [SerializeField] string locationName;   // ex) 장소가 복도이고 문이 여러 개 있을 때, 각각의 문에 다른 이름이 있다.

    public string GetSceneName()
    {
        return sceneName;
    }

    public string GetLocationName()
    {
        return locationName;
    }
}
