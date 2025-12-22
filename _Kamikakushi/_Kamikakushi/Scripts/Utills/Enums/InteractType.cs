using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Enums
{
    public enum InteractType
    {
        Event, //책같은 내용 확인 / 다른 아이템과 상호작용하는 종류의 타입
        PickUp, //획득가능한 아이템
        Door, //문열기 / 닫기
        Hide //숨을 수 있는 아이템
             //현재는 숨기 기능을 플레이어가 직접 상호작용할 때만 발생하지만
             //외부에서 숨기기능이 필요할 시 인터페이스로 확장 고려중
    }
}
