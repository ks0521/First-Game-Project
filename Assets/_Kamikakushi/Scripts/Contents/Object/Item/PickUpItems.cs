using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Enums;
using Project.Inventory;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Contents.Item
{
    public abstract class PickUpItems : MonoBehaviour, IInteractable
    {
        //실제 인벤토리에 저장될 아이템 정보
        [SerializeField] protected ItemData data;
        //UI나 크로스헤어 정보전달용
        protected InteractContext context;
        //상호작용 결과 반환
        protected InteractResult result;
        private void Awake()
        {
            //아래 2줄은 고정(PickupItem확정, 위치는 안씀)
            context.promptKey = PromptKey.PickupItem;
            result.transform = null;
            Init();
        }
        abstract protected void Init();

        /// <summary>
        /// 상호작용 조건들을 모두 만족하는지 확인
        /// </summary>
        /// <param name="target">플레이어의 정보</param>
        /// <returns>모든 조건을 만족 시 True, 아니면 False 반환</returns>
        public bool CanInteract(PlayerManager target)
        {
            return true;
        }
        //상호작용 결과 구현
        abstract public InteractResult Interact(PlayerManager target);

        //InteractContext 구조체 반환하여 UI부에서 크로스헤어 및 텍스트 출력
        public InteractContext GetContext()
        {
            return context;
        }
    }

}

