using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Wardrobe : InteractItems, IInteractable
    {
        [Header("Hide Settings")]
        [SerializeField] private Transform hidePoint;
        private bool isHiding = false;

        protected override void Init()
        {
            explain = "E : ¼û±â";
        }

        public override bool CanInteract(PlayerManager target)
        {
            if (!base.CanInteract(target))
                return false;

            return true;
        }

        public bool Interact(PlayerManager target)
        {
            if (!CanInteract(target))
                return false;

            if (!isHiding)
            {
                // ¼û±â ÁøÀÔ
                target.transform.position = hidePoint.position;
                isHiding = true;
                Debug.Log("Àå·Õ ¾ÈÀ¸·Î ¼û¾ú´Ù.");
            }
            else
            {
                // ¼û±â ÇØÁ¦
                isHiding = false;
                Debug.Log("Àå·Õ¿¡¼­ ³ª¿Ô´Ù.");
            }

            return true;
        }
    }
}
