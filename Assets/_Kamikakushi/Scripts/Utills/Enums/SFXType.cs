using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Utills.Audio
{
    public enum SFXType
    {
        None = 0,
        
        //오디오
        PickupItem, //아이템줍기
        DrinkWater, //물마시기
        UseItem, //아이템사용하기(열쇠와 장난감을 창고와 호수에 사용할때 사용할사운드)
        DoorOpen, // 문여는소리(문닫는소리는 이걸로 재활용)
        HideEnter, //숨을때 덜컹이나 끼익거리는 소리
        HideBreath, //플레이어 숨었을때 거친숨소리 조금 작게 출력시켜서 두려워서 숨이 거칠게나오지만 참으려는 느낌으로
        Heartbeat // 심장소리 거리에비례해 사운드조절 Loop시켜서 반복되게
    }
}
