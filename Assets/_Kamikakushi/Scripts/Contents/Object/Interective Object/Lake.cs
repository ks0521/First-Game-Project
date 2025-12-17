using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Structs;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Lake : InteractItems
    {
        private bool isUsed = false;

        protected override void Init()
        {
            context.displayName = "호수";
            context.promptKey = PromptKey.UseItem;
        }

        public override bool CanInteract(PlayerManager target)
        {
            if (isUsed)
                return false;

            return base.CanInteract(target);
        }

        public override InteractResult Interact(PlayerManager target)
        {
            if (!CanInteract(target))
                return InteractResult.Fail("아무 일도 일어나지 않는다.");

            // 여기서는 '성공했을 때 무슨 일이 일어나는지만' 작성
            isUsed = true;

            Debug.Log("호수에 아이템을 사용했다.");

            return InteractResult.Success("장난감을 호수에 던졌다.");
        }
    }
}
