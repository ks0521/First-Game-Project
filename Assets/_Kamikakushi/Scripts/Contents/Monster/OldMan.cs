using UnityEngine;

namespace Assets._Kamikakushi.Contents.Monster
{
    public class OldMan : MentalMonster
    {
        protected override void Awake()
        {
            speed = 2f; // 느린 몬스터
            base.Awake();
        }
    }
}
