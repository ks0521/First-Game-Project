using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using DoorScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Book_S1 : InteractItems
    {
        //이벤트 진행이 되었는지 판정(이미 진행됬으면 또 읽어도 이벤트 진행이 되지않음 - 중복방지)
        bool isTriggered;
        [SerializeField] GameObject ghost;
        [SerializeField] Door door;
        protected override void Init()
        {
            //인터페이스의 배열이기때문에 GetComponents 사용
            interactType = InteractType.Event;
            
            context.promptKey = PromptKey.Inspect;
            context.displayName = "책";
        }
        public override InteractResult Interact(PlayerManager target)
        {
            result.success = true;
            result.message = "문이 열리는소리가 들린다...\n 숨어야하나?";

            if (!isTriggered)
            {
                //책읽기 액션 추가
                ghost.SetActive(true);
                door.Open();
                isTriggered = true;
            }
            return result;
        }
    }
}
