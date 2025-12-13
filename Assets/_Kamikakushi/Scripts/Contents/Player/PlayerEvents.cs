using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    /// <summary>
    /// 플레이어에게서 발생하는 모든 이벤트를 발행하는 창구
    /// </summary>
    public class PlayerEvents : MonoBehaviour
    {
        /// <summary>
        /// 플레이어 피격 이벤트, 받은 데미지 인자로 전달
        /// </summary>
        public event Action<float> PlayerHitEvent;
        /// <summary>
        /// InteractableObject를 레이캐스트 성공시 발생, 탐지한 오브젝트 인자로 전달
        /// </summary>
        public event Action<RaycastHit> RaycastEnter;
        /// <summary>
        /// InteractableObject에서 레이캐스트가 떨어졌을 시 발생
        /// </summary>
        public event Action RaycastOut;
        public void OnHit(float damage)
        {
            PlayerHitEvent?.Invoke(5);
        }
        public void OnRaycastEnter(RaycastHit hit)
        {
            Debug.Log("상호작용 성공");
            RaycastEnter?.Invoke(hit);
        }
        public void OnRaycastOut()
        {
            Debug.Log("시선 떨어짐");
            RaycastOut?.Invoke();
        }
    }
}

