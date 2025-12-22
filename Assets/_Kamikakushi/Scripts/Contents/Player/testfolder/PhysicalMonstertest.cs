using UnityEngine;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Enums;
using System;

namespace _Kamikakushi.Contents.Player.Test
{
    abstract public class PhysicalMonsterTest : MonsterTest
    {
        public override void Move(Vector3 targetPos)
        {
            if (agent == null) return;
            agent.SetDestination(targetPos);
        }
        /*
        public virtual bool Hit(Vector3 targetPos)
        {
            // 근접 공격 처리
            Debug.Log($"{name} Physical Hit!");
            return true;
        }*/
    }
}
