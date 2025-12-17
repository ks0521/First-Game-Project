using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class WardrobeHide : InteractItems, IInteractable
    {
        [SerializeField] private Transform hidePoint;
        private bool isHiding;

        protected override void Init()
        {
        }

        public override bool CanInteract(Player.PlayerManager target)
        {
            return base.CanInteract(target);
        }

        public bool Interact(Player.PlayerManager target)
        {
            if (!CanInteract(target))
                return false;

            if (!isHiding)
            {
                target.transform.position = hidePoint.position;
                isHiding = true;
                Debug.Log("[Wardrobe] ¼û±â ÁøÀÔ");
            }
            else
            {
                isHiding = false;
                Debug.Log("[Wardrobe] ¼û±â ÇØÁ¦");
            }

            return true;
        }
    }
}
