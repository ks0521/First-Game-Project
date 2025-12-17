using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Structs;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Painting : InteractItems
    {
        [TextArea]
        [SerializeField] private string inspectText;

        protected override void Init()
        {
            interactType = InteractType.Event;
            context.displayName = "±×¸²";
            context.promptKey = PromptKey.Inspect; 

            result.success = true;
        }

        public override bool CanInteract(PlayerManager target)
        {
            return true;
        }

        public override InteractResult Interact(PlayerManager target)
        {
            result.success = true;
            result.message = inspectText;
            return result;
        }
    }
}
