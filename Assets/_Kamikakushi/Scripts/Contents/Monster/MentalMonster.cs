using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class MentalMonster : Monster
    {
        public override void Move(Vector3 targetPos)
        {
            if (agent == null) return;
            agent.SetDestination(targetPos);
        }

        public virtual bool Hit(Vector3 targetPos)
        {
            // 멘탈 타입 공격 처리
            Debug.Log($"{name} Mental Attack!");
            return true;
        }
    }
}
