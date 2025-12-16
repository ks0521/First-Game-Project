using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using Project.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public struct interactAttemptinfo
    {
        public InteractType type;
        public string text;
    }
    /// <summary>
    /// 플레이어에게서 발생하는 모든 이벤트를 발행하는 창구
    /// </summary>
    public class PlayerEvents : MonoBehaviour
    {
        public event Action<ItemData> ItemPickUp;
        /// <summary>
        /// 플레이어 피격 이벤트, 받은 데미지 인자로 전달
        /// </summary>
        public event Action<float> PlayerHitEvent;
        /// <summary>
        /// InteractableObject를 레이캐스트 성공시 발생, 
        /// 탐지한 오브젝트의 정보를 전달
        /// </summary>
        public event Action<InteractContext> GetInteractContext;
        public event Action<InteractResult> GetInteractResult;
        public event Action<IInteractable> GetInteractable;
        //public event Action<interactAttemptinfo> info;
        /// <summary>
        /// InteractableObject에서 레이캐스트가 떨어졌을 시 발생
        /// </summary>
        public event Action RaycastOut;
        public void OnHit(float damage)
        {
            PlayerHitEvent?.Invoke(5);
        }
        public void OnFindInteractable(IInteractable interactable)
        {
            GetInteractable?.Invoke(interactable);
        }
        public void OnRaycastEnter(InteractContext context)
        {
            Debug.Log("상호작용 탐지 성공");
            GetInteractContext?.Invoke(context);
        }
        public void OnRaycastOut()
        {
            Debug.Log("시선 떨어짐");
            RaycastOut?.Invoke();
        }
        public void OnInteract(InteractResult result)
        {
            GetInteractResult?.Invoke(result);
        }
        public void OnPickUp(ItemData data)
        {
            ItemPickUp?.Invoke(data);
        }
    }
}

