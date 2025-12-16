using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class OldMan : MentalMonster
    {
        protected override void Awake()
        {
            movementType = MovementType.Transform;
            speed = 2f; // 느린 몬스터 //이 부분을 수정해주세요
            base.Awake();
        }
    }
}
