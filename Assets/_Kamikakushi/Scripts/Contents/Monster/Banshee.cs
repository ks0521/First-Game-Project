using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    //보통의 속도로 추적하는 피지컬몬스터
    public class Banshee : PhysicalMonster
    {
        protected override void Awake()
        {
            
            speed = 4.5f; // 빠른 몬스터 //이 부분을 수정해주세요
            base.Awake();
        }
    }
}
