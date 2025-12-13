using _Kamikakushi.Contents.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Interfaces

{
    public interface IInteractable
    {
        //상호작용 가능한 아이템에 달리는 인터페이스
        public bool CanInteract(PlayerManager target);
        public void Interact(PlayerManager target);
    }
}
