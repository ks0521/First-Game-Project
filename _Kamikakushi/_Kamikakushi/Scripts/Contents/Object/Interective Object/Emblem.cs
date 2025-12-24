using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Structs;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Emblem : InteractItems
    {
        [TextArea]
        [SerializeField] private string inspectText;

        protected override void Init()
        {
            context.promptKey = PromptKey.Inspect;
            context.displayName = "πÆ¿Â";
        }

        public override bool CanInteract(PlayerManager target)
        {
            return base.CanInteract(target);
        }

        public override InteractResult Interact(PlayerManager target)
        {
            result.success = true;
            result.message = inspectText;
            return result;
        }
    }
}
