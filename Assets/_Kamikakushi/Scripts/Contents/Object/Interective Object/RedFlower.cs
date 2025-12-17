using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using UnityEngine;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Contents.Item
{
    public class RedFlower : PickUpItems
    {
        [TextArea]
        [SerializeField] private string inspectText;

        protected override void Init()
        {
            context.displayName = "∫”¿∫ ≤…";
            context.promptKey = PromptKey.PickupItem;
        }

        public override InteractResult Interact(PlayerManager target)
        {
            // ¿Œ∫•≈‰∏Æ √ﬂ∞° Ω√µµ
            if (!target.inven.Add(data))
            {
                result.success = false;
                result.message = "∞°πÊ¿Ã ≤À √°¥Ÿ...";
                return result;
            }

            // ¡∂ªÁ ∏ﬁΩ√¡ˆ + »πµÊ
            result.success = true;
            result.message = inspectText;

            Debug.Log("∫”¿∫ ≤… »πµÊ");
            Destroy(gameObject);

            return result;
        }
    }
}
