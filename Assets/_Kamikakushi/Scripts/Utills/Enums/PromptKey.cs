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
        OpenDoor, //특정아이템필요없는 문열기
        LockDoor, //특정아이템이필요한 문열기
        CloseDoor, //문닫기

        //아이템관련
        PickupItem, //아이템 줍기
        UseItem, //아이템 사용하기

        //환경 상호작용
        AcivateSwitch, //스위치(필요할수도있을것같아서)
        Insprct, //조사

        //숨기
        Hide, //숨기
        HideTransform //숨기후 카메라 시점이동 (장롱틈으로 시선이동하는 느낌)
    }
}