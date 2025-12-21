using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Enums;
using Project.Inventory;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class PickUpItems : MonoBehaviour, IInteractable
    {
        //실제 인벤토리에 저장될 아이템 정보
        [SerializeField] protected ItemData data;
        //UI나 크로스헤어 정보전달용
        protected InteractContext context;
        //상호작용 결과 반환
        protected InteractResult result;
        private void Awake()
        {
            //이름은 os에서 따옴
            context.displayName = data.itemName ?? "사용아이템";
            context.promptKey = PromptKey.PickupItem;
            result.actions = new List<IInteractAction>();  
        }
        public InteractResult Interact(PlayerManager target)
        {
            if (!target.inven.Add(data))
            {
                result.success = false;
                result.message = "가방이 꽉 찼다...";
                Debug.Log("아이템 획득 실패");
                return result;
            }
            Debug.Log("인벤토리 추가");
            Destroy(gameObject);
            result.success = true;
            result.message = context.displayName + " 획득";
            return result;
        }

        /// <summary>
        /// 상호작용 조건들을 모두 만족하는지 확인
        /// </summary>
        /// <param name="target">플레이어의 정보</param>
        /// <returns>모든 조건을 만족 시 True, 아니면 False 반환</returns>
        public bool CanInteract(PlayerManager target)
        {
            return true;
        }

        //InteractContext 구조체 반환하여 UI부에서 크로스헤어 및 텍스트 출력
        public InteractContext GetContext()
        {
            return context;
        }
    }

}

