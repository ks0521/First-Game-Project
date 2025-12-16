using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Book : InteractItems, IInteractable
    {
        [TextArea]
        [SerializeField] private string inspectText;

        protected override void Init()
        {
            // UI 표현은 Context에서 처리
        }

        public override bool CanInteract(PlayerManager target)
        {
            return base.CanInteract(target);
        }

        public bool Interact(PlayerManager target)
        {
            if (!CanInteract(target))
                return false;

            // 실제 게임에선 UI 시스템이 이 텍스트를 사용
            Debug.Log($"[BookInspect] 조사 내용: {inspectText}");

            return true;
        }
    }
}
