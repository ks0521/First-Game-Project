using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Utills.Enums
{
    public enum PromptKey
    {
        None = 0,

        //문관련
        OpenDoor, // (E : 문열기) 문을열었다 (위쪽화살표 모양 크로스헤어)
        LockDoor, // (E : 문열기) 열쇠를 사용했다 (자물쇠열린 모양 크로스헤어)
        CloseDoor, // (E : 문닫기) 문을닫았다 (아래쪽화살표 모양 크로스헤어)


        PickupItem, // (E : 줍기) "??"를 주웠다 (돋보기 + 모양 크로스헤어)
        UseItem, // (아이템사용) 물같은 경우에는 사용시 ui 사용했다정도만 알수있게 출력해주세요 ex:물을 마셨다 이외에 사용되는 아이템은 창고,호수에
                 //열쇠, 장난감 아이템 사용되는데 이건 사용시 이벤트발생해서 텍스트출력되게해주세요


        AcivateSwitch, //스위치를 사용할일이있을까 싶어 추가한건데 왠지없을것같습니다 만약에있다면 (E : 불켜기) 불을켜자 (해모양 크로스헤어)
        Inspect, // (E : 조사) "??"를 발견했다 (눈이 번쩍하는 모양 크로스헤어)


        Hide, // (ctrl : 숨기) 숨었다같은 텍스트 출력x (눈이 반쯤 감긴모양 크로스헤어)
              // 이후 플레이어가 숨었을때 크로스헤어 눈에 슬래시 있는 크로스헤어로 변경해주세요
        HideTransform
    }
}