using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Structs;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class ToyItem : InteractItems
    {
        [SerializeField] private string itemKeyCode = "toy";

        protected override void Init()
        {
            interactType = InteractType.Event;
            context.promptKey = PromptKey.Inspect;
            context.displayName = "장난감";
        }

        public override InteractResult Interact(PlayerManager target)
        {
            if (target.handeditems != null)
            {
                target.handeditems.keyCode = itemKeyCode;
            }
            else
            {
                // 만약 handeditems 자체가 null이면 새로 할당하거나 처리하는 로직이 필요할 수 있음
                // 예: target.EquipItem(itemKeyCode);
            }

            result.success = true;
            result.message = "장난감을 발견했다";

            Destroy(gameObject);

            return result;
        }
    }
}