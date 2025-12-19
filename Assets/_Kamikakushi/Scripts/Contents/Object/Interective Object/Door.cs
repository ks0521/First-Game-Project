using _Kamikakushi.Contents.InteractAction;
using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using DoorScript;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Door : InteractItems
    {
        [SerializeField] NavMeshObstacle obstacle;
        ToggleDoor door;
        bool isLocked;
        bool isOpened;
        [SerializeField] string overrideOpenText;
        [SerializeField] string overrideCloseText;
        [SerializeField] string overrideLockedText;
        protected override void Init()
        {
            door = GetComponent<ToggleDoor>();
            //인터페이스의 배열이기때문에 GetComponents 사용
            interactType = InteractType.Door;
            conditions = GetComponents<IInteractionCondition>();
            //개방 조건이 하나라도 있으면 잠김
            if (conditions.Length == 0) isLocked = false;
            else isLocked = true;
            //만약 처음부터 열려있다면 true로 바꾸기
            isOpened = false;
            interactType = InteractType.Event;

            context.promptKey = PromptKey.LockDoor;
            context.displayName = "문";
        }
        public override bool CanInteract(PlayerManager target)
        {
            if (!base.CanInteract(target))
            {
                return false;
            }
            //오브젝트 특성별 interact 조건 추가
            return true;

        }

        public override InteractResult Interact(PlayerManager target)
        {
            if (isLocked)
            {
                if (!CanInteract(target))
                {
                    result.success = false;
                    result.message = MessageChoice(
                        overrideLockedText,
                        "문이 잠겨있는것 같다...");
                    return result;
                }
                else
                {
                    isLocked = false;
                    result.success = true;
                    result.actions.Add(new ConsumeEquippedItemAction());
                    result.message = MessageChoice(
                        overrideOpenText,
                        "잠긴 문을 열었다");
                    Open();
                    return result;
                }
            }
            else
            {
                //닫혀있으면
                if(!isOpened)
                {
                    result.success = true;
                    result.message = MessageChoice(
                        overrideOpenText,
                        "문을 열었다");
                    Open();
                    return result;
                    //문열림
                }
                else
                {
                    result.success = true;
                    result.message = MessageChoice(
                        overrideCloseText,
                        "문을 닫았다");
                    Close();
                    return result;
                    //문닫힘
                }
            }
        }
        private string MessageChoice(string overrideText, string defaultText)
        {
            return string.IsNullOrEmpty(overrideText) ? defaultText : overrideText;
        }
        public void Open()
        {
            door.Toggle();
            isOpened = true;
            context.promptKey = PromptKey.CloseDoor;
            ObstacleChange(false);
        }
        public void Close()
        {
            door.Toggle();
            isOpened = false;
            context.promptKey = PromptKey.CloseDoor;
            ObstacleChange(true);
        }
        void ObstacleChange(bool value)
        {
            if(obstacle != null) obstacle.enabled = value;
        }
    }
}
