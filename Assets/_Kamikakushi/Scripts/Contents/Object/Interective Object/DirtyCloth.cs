using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using UnityEngine;
using _Kamikakushi.Utills.Structs;
using _Kamikakushi.Contents.InteractAction;

namespace _Kamikakushi.Contents.Item
{
    public class DirtyCloth : PickUpItems
    {
        [TextArea]
        [SerializeField] private string inspectText;

        protected override void Init()
        {
            context.displayName = "더러운 천";
            context.promptKey = PromptKey.PickupItem;
        }

        public override InteractResult Interact(PlayerManager target)
        {
            // 인벤토리 추가 시도
            if (!target.inven.Add(data))
            {
                result.success = false;
                result.message = "가방이 꽉 찼다...";
                return result;
            }

            // 조사 메시지 + 획득
            result.success = true;
            result.message = inspectText;

            Debug.Log("더러운 천 획득");
            //픽업 액션 추가
            result.actions.Add(new PickUpItemAction(data));

            return result;
        }
    }
}
