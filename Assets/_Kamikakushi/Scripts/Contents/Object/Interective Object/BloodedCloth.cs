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
    public class BloodedCloth : InteractItems
    {
        //이벤트 진행이 되었는지 판정(이미 진행됬으면 또 읽어도 이벤트 진행이 되지않음 - 중복방지)
        bool isTriggered;
        protected override void Init()
        {
            //인터페이스의 배열이기때문에 GetComponents 사용
            interactType = InteractType.Event;
            
            context.promptKey = PromptKey.Inspect;
            context.displayName = "피묻은 천";
        }
        public override InteractResult Interact(PlayerManager target)
        {
            result.success = true;
            result.message = "피가 말라붙어 있는 더러운 천이다";

            if (!isTriggered)
            {
                //특정 액션하고 trigger = true;
            }
            //아니면 result.actions.add(여기에 특정 트리거 추가)
            return result;
        }
    }
}
