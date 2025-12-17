using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Interfaces
{
    public interface IHittable
    {
        //플레이어에게만 달리는 피격 인터페이스
        public void Hit(Vector3 position, float damage, float time, HitType type);
    }
}