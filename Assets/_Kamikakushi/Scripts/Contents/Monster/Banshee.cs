using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class Banshee : PhysicalMonster
    {
        protected override void Awake()
        {
            movementType = MovementType.NavMesh;
            speed = 4.5f; // 빠른 몬스터 //이 부분을 수정해주세요
            base.Awake();
        }
    }
}
