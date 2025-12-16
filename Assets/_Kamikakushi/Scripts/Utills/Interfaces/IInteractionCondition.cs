using _Kamikakushi.Contents.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Interfaces
{
    public interface IInteractionCondition
    {
        //상호작용 조건부분 분리
        //해당 인터페이스를 상속받는 클래스는 다양한 조건설정 가능
        //player가 장착한 아이템 keycode뿐만이 아니라 체력, 진척도 등...
        bool CanInteract(PlayerManager target);
    }
}
