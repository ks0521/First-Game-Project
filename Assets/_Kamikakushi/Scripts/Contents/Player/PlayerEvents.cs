using _Kamikakushi.Utills.Enums;
using Project.Inventory;
using System;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerEvents : MonoBehaviour
    {
        public event Action<ItemData> ItemPickUp;
        public event Action<float> PlayerHitEvent;
        public event Action<RaycastHit> RaycastEnter;
        public event Action RaycastOut;

        public void OnHit(float damage)
        {
            PlayerHitEvent?.Invoke(5);
        }

        public void OnRaycastEnter(RaycastHit hit)
        {
            Debug.Log("상호작용 탐지 성공");
            RaycastEnter?.Invoke(hit);
        }

        public void OnRaycastOut()
        {
            Debug.Log("시선 떨어짐");
            RaycastOut?.Invoke();
        }

        // 수정
        public void OnPickUp(ItemData data)
        {
            ItemPickUp?.Invoke(data);
        }
    }
}