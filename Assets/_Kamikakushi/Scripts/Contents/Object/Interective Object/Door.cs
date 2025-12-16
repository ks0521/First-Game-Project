using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills;
using _Kamikakushi.Contents.Player;
using UnityEngine;
using _Kamikakushi.Contents.Item;
using _Kamikakushi.Utills.Enums;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Door : InteractItems, IInteractable
    {
        protected override void Init()
        {
            //인터페이스의 배열이기때문에 GetComponents 사용
            explain = "E : 사용";
            interactType = InteractType.Door;
        }
        public override bool CanInteract(PlayerManager target)
        {
            if(!base.CanInteract(target))
            {
                return false;
            }

            //오브젝트 특성별 interact 조건 추가
            return true;

        }
        public bool Interact(PlayerManager target)
        {
            if (CanInteract(target))
            {
                Debug.Log("상호작용 했습니다.");
                return true;
            }
            return false;
        }
    }
}
