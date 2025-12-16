using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    /// <summary>
    /// 플레이어 피격처리
    /// </summary>
    public class PlayerHit : MonoBehaviour, IHittable
    {
        [SerializeField] private PlayerEvents events;
        //플레이어가 피격받을 때 invoke(변수는 피격판정 지속시간)
        Vector3 targetPos;
        // targetPosition 방향(정규화됨)
        Vector3 normalizedDirect;
        public void Start()
        {
            events = GetComponent<PlayerEvents>();
        }
        public void Hit(Vector3 targetPosition)
        {
            normalizedDirect = (targetPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(normalizedDirect);
            events.OnHit(5);
        }
    }
}

