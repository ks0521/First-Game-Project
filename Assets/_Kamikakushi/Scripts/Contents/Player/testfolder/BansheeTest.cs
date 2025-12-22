using _Kamikakushi.Contents.Monster;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

namespace _Kamikakushi.Contents.Player.Test
{
    public class BansheeTest : PhysicalMonsterTest
    {
        protected override void Awake()
        {
           
            speed = 4.5f; // 빠른 몬스터 //이 부분을 수정해주세요
            base.Awake();
            //OnPlayerHit += Hit;
        }
        protected override void Hit(IHittable hit)
        {
            hit.Hit(transform.position, 30, 2, HitType.Physical);
        }
    }
}
