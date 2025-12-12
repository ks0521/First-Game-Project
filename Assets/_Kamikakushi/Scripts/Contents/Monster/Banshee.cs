using UnityEngine;

namespace Assets._Kamikakushi.Contents.Monster
{
    public class Banshee : PhysicalMonster
    {
        protected override void Awake()
        {
            speed = 4.5f; // 빠른 몬스터
            base.Awake();
        }
    }
}
