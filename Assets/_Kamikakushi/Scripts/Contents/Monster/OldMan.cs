using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    //느린속도로 벽을 뚫는 멘탈몬스터
    public class OldMan : MentalMonster
    {
        protected override void Awake()
        {
            
            speed = 2f; // 느린 몬스터 //이 부분을 수정해주세요
            base.Awake();
        }
    }
}
