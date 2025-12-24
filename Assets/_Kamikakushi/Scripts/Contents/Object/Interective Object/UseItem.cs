using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Enums;
using Project.Inventory;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;
using System;
using _Kamikakushi.Contents.InteractAction;
using _Kamikakushi.Utills.Audio;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class UseItem : MonoBehaviour, IInteractable
    {
        //UI나 크로스헤어 정보전달용
        protected InteractContext context;
        [SerializeField] private string itemName;
        [SerializeField] private string resultMessage;
        [SerializeField] private float increseHp;
        [SerializeField] private float increseSanity;

        //상호작용 결과 반환
        [SerializeField] protected InteractResult result;
        private void Awake()
        {
            //이름은 os에서 따옴
            context.displayName = itemName ?? "사용아이템";
            context.promptKey = PromptKey.UseItem;
            result.actions = new List<IInteractAction>();
            result.actions.Add(new PlayerHpRecovery(increseHp));
            result.actions.Add(new PlayerSanityRecovery(increseSanity));
            result.actions.Add(new PlaySFXAction(SFXType.UseItem));
        }
        public InteractResult Interact(PlayerManager target)
        {
            
            Debug.Log("플레이어 회복");
            result.success = true;
            result.message = resultMessage;
            Destroy(gameObject);
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

