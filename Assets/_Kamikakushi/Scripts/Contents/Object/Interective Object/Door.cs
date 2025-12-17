using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Door : InteractItems
    {
        protected override void Init()
        {
            //인터페이스의 배열이기때문에 GetComponents 사용
            interactType = InteractType.Door;
            conditions = GetComponents<IInteractionCondition>();
            interactType = InteractType.Event;

            context.promptKey = PromptKey.CloseDoor;
            context.displayName = "문";
            result.transform = null;
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

        public override InteractResult Interact(PlayerManager target)
        {
            if (!CanInteract(target))
            {
                result.success = false;
                result.message = "문이 잠겨있는것 같다...";
                return result;
            }
            //열쇠를 이용하거나 열쇠가 필요없거나 어쨋든 열었기 때문에 조건을 항상 true로 해줌
            if(context.promptKey == PromptKey.CloseDoor && CanInteract(target))
            {
                context.promptKey = PromptKey.OpenDoor;
                Destroy(GetComponent<RequireItemKeyCode>());
                conditions = GetComponents<IInteractionCondition>();
                result.success = true;
                result.message = "문을 열었다.";

                Debug.Log("문열림");
                return result;
                //문열림 및 플레이어가 이동 가능
            }
            if(context.promptKey == PromptKey.OpenDoor)
            {
                context.promptKey = PromptKey.CloseDoor;
                result.success = true;
                result.message = "문을 닫았다";
                Debug.Log("문닫힘");
                return result;
                //문닫힘 및 플레이어 이동 불가
            }
            Debug.Log("오류발생");
            result.success = false;
            result.message = "";
            return result;
        }
    }
}
