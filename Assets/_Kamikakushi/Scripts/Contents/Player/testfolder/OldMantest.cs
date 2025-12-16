using _Kamikakushi.Contents.Monster;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class OldMantest : MentalMonstertest
    {
        protected override void Awake()
        {
            movementType = MovementTypetest.Transform;
            speed = 2f; // 느린 몬스터
            base.Awake();
        }
    }
}
