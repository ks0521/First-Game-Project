using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Contents.Item
{
    public class Water : InteractItems
    {
        bool isDrink;
        protected override void Init()
        {
            //인터페이스의 배열이기때문에 GetComponents 사용
            conditions = GetComponents<IInteractionCondition>();
            interactType = InteractType.Event;
            context.promptKey = PromptKey.Inspect;
            context.displayName = "물";
            result.success = true;
            isDrink = true;
        }
        public override bool CanInteract(PlayerManager target) { return isDrink; }
        public override InteractResult Interact(PlayerManager target)
        {
            if (CanInteract(target))
            {
                //혹은 사용시 destroy?
                result.message = "이미 마신 물이다...";
                //두번은 못마심 -> 이부분 수정필요
                result.success = false;
                return result;
            }
            else
            {
                //마시면 정신력을 회복시켜주는 오브젝트
                target.stat.sanity += 20;
                isDrink = false;
                result.success = true;
                result.message = "정신이 살짝 맑아진다.... 정신력 20 회복!";
                return result;
            }
        }
    }

}
