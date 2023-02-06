using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;   // 이미지를 서서히 변경

    // 현재 sprite와 변경하고자 하는 sprite가 같은 경우, 변경할 필요가 X
    bool CheckSameSprite(Image p_image, Sprite p_sprite)
    {
        if (p_image.sprite == p_sprite)
            return true;
        else
            return false;
    }

    //// p_target: 어떤 이미지를 변경할 것인지, p_spriteName: 어떤 이미지로 변경할 것인지
    //public IEnumerator SpriteChangeCoroutine(Transform p_target, string p_spriteName)
    //{
    //    // 1. t_image 이미지를 변경
    //    // Standing Image 오브젝트에는 Image 컴포넌트 X → 그 자식인 Image 오브젝트에는 Image 컴포넌트 O
    //    Image t_image = p_target.GetComponentInChildren<Image>();

    //    // 2. t_sprite 이미지로 변경
    //    // Characters 폴더에 있는 이미지를 가져와 Sprite 타입으로 변경 가능한지 검증한 후, Sprite로 강제 형변환
    //    p_spriteName = p_spriteName.Trim();

    //    if (p_spriteName != "")
    //    {
    //        Sprite t_sprite = Resources.Load("Images/Characters/" + p_spriteName, typeof(Sprite)) as Sprite;
    //        // Sprite t_sprite = Resources.Load<Sprite>("Images/Characters/" + p_spriteName);

    //        Debug.Log(p_spriteName);
    //        Debug.Log(t_sprite);

    //        // 두 이미지가 같지 않으면 새 이미지로 변경
    //        if (!CheckSameSprite(t_image, t_sprite))
    //        {
    //            // 1. 현재 이미지를 지우기
    //            Color t_color = t_image.color; // 현재 이미지의 color 속성
    //            t_color.a = 0;  // 투명도를 0으로
    //            t_image.color = t_color;       // 바뀐 투명도를 적용

    //            // 2. 새 이미지를 보여주기
    //            t_image.sprite = t_sprite;     // 이미지 교체

    //            while (t_color.a < 1)
    //            {
    //                t_color.a += fadeSpeed; // 투명도를 0에서부터 서서히 올리기
    //                t_image.color = t_color;   // 바뀐 투명도를 적용

    //                yield return null;  // 1프레임 대기
    //            }
    //        }
    //    }

    //    else
    //    {
    //        // 1. 현재 이미지를 지우기
    //        Color t_color = t_image.color; // 현재 이미지의 color 속성
    //        t_color.a = 0;  // 투명도를 0으로
    //        t_image.color = t_color;       // 바뀐 투명도를 적용
    //    }
    //}

    // p_target: 어떤 이미지를 변경할 것인지, p_spriteName: 어떤 이미지로 변경할 것인지
    public IEnumerator SpriteChangeCoroutine(Transform p_target, string p_spriteName)
    {
        // 1. t_image 이미지를 변경
        // Standing Image 오브젝트에는 Image 컴포넌트 X → 그 자식인 Image 오브젝트에는 Image 컴포넌트 O
        Image t_image = p_target.GetComponentInChildren<Image>();

        // 2. t_sprite 이미지로 변경
        // Characters 폴더에 있는 이미지를 가져와 Sprite 타입으로 변경 가능한지 검증한 후, Sprite로 강제 형변환
        p_spriteName = p_spriteName.Trim(); // 공백 제거
        Sprite t_sprite = Resources.Load("Images/Characters/" + p_spriteName, typeof(Sprite)) as Sprite;


        // 두 이미지가 같지 않으면 새 이미지로 변경
        if (!CheckSameSprite(t_image, t_sprite))
        {
            // 1. 현재 이미지를 지우기
            Color t_color = t_image.color; // 현재 이미지의 color 속성
            t_color.a = 0;  // 투명도를 0으로
            t_image.color = t_color;       // 바뀐 투명도를 적용

            // 2. 새 이미지를 보여주기
            t_image.sprite = t_sprite;     // 이미지 교체

            while (t_color.a < 1)
            {
                if (t_sprite != null)
                {
                    t_color.a += fadeSpeed; // 투명도를 0에서부터 서서히 올리기
                    t_image.color = t_color;   // 바뀐 투명도를 적용
                }

                yield return null;  // 1프레임 대기
            }
        }
    }
}
