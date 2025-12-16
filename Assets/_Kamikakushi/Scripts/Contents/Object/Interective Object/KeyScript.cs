using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

namespace _Kamikakushi.Contents.Item
{
    public class KeyPickup : InteractItems, IInteractable
    {
        protected override void Init()
        {
        }

        public override bool CanInteract(PlayerManager target)
        {
            return base.CanInteract(target);
        }

        public bool Interact(PlayerManager target)
        {
            if (!CanInteract(target))
                return false;

            gameObject.SetActive(false);
            return true;
        }
    }
}
