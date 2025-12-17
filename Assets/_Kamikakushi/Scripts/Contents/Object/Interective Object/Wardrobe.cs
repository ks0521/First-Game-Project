using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Wardrobe : InteractItems, IInteractable
    {
        [Header("Hide Settings")]
        //이벤트 진행이 되었는지 판정(이미 진행됬으면 또 읽어도 이벤트 진행이 되지않음 - 중복방지)
        bool isTriggered;
        protected override void Init()
        {
            //인터페이스의 배열이기때문에 GetComponents 사용
            conditions = GetComponents<IInteractionCondition>();
            interactType = InteractType.Event;

            context.promptKey = PromptKey.Inspect;
            context.displayName = "책";
            result.transform = null;
        }
        public override InteractResult Interact(PlayerManager target)
        {
            result.success = true;
            result.message = "내용을 확인해볼까?";
            if (!isTriggered)
            {
                //여기서 책을 읽는 내용이나 스토리 진행 이벤트 실행시키기
                isTriggered = true;
            }

            return result;
        }
    }
}
