using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Interfaces

{
    public interface IInteractable
    {
        //상호작용 가능한 아이템에 달리는 인터페이스
        //상호작용이 가능하다면 true 반환
        public InteractResult Interact(PlayerManager target);
        public InteractContext GetContext();
    }
}
