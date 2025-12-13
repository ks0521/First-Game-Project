using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerHit : MonoBehaviour, IHittable
    {
        //플레이어가 피격받을 때 invoke(변수는 피격판정 지속시간)
        public event Action<float> PlayerHitEvent;
        Vector3 targetPos;
        // targetPosition 방향(정규화됨)
        Vector3 normalizedDirect;
        public void Start()
        {
            
        }
        public void Hit(Vector3 targetPosition)
        {
            normalizedDirect = (targetPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(normalizedDirect);
            PlayerHitEvent?.Invoke(5);
        }
    }
}

