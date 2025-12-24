using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Enums;
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
        private PlayerEvents events;
        private PlayerManager manager;
        public void Start()
        {
            events = GetComponent<PlayerEvents>();
            manager = GetComponent<PlayerManager>();
        }
        public void Hit(Vector3 targetPosition, float damage, float time, HitType type)
        {
            //숨은 상태에서 충돌시 피격당하지 않음
            if (manager.isHide) return;
            events.OnHit(targetPosition, damage, time, type);
        }
    }
}

