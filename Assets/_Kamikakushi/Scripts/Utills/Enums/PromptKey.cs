using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Utills.Structs
{
    public enum PromptKey
    {
        None = 0,

        //문관련
        OpenDoor, //특정아이템필요없는 문열기 (E : 문열기), (위쪽화살표모양 크로스헤어)
        LockDoor, //특정아이템이필요한 문열기 (E : 열쇠사용),  (열쇠모양 크로스헤어)
        CloseDoor, //문닫기 (E : 문닫기), (아래쪽화살표모양 크로스헤어)

        //아이템관련
        PickupItem, //아이템 줍기 (F : 습득), (눈뜬 모양 크로스헤어)
        UseItem, //아이템 사용하기

        //환경 상호작용
        AcivateSwitch, //스위치(필요할수도있을것같아서)
        Insprct, //조사 (F : 조사), (눈뜬 모양 크로스헤어)

        //숨기
        Hide, //숨기 (Ctrl : 숨기), (눈반쯤 감은 모양 크로스헤어)
        HideTransform //숨기후 카메라 시점이동 (장롱틈으로 시선이동하는 느낌)
    }
}