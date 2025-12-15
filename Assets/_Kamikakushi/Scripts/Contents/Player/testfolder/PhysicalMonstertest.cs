using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PhysicalMonstertest : Monstertest
    {
        public override void Move(Vector3 targetPos)
        {
            if (agent == null) return;
            agent.SetDestination(targetPos);
        }

        public virtual bool Hit(Vector3 targetPos)
        {
            // 근접 공격 처리
            Debug.Log($"{name} Physical Hit!");
            return true;
        }
    }
}
