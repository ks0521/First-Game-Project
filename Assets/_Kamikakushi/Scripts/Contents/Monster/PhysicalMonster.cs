using UnityEngine;
using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Interfaces;

namespace _Kamikakushi.Contents.Monster
{
    public class PhysicalMonster : Monster
    {
        public override void Move(Vector3 targetPos)
        {
            if (agent == null) return;
            agent.SetDestination(targetPos);
        }
        IHittable target;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("트리거 충돌");
            if (other.TryGetComponent<IHittable>(out target))
            {
                Debug.Log("플레이어 충돌");
                target.Hit(transform.position);
            }
        }/*
        public virtual bool Hit(Vector3 targetPos)
        {
            // 근접 공격 처리
            Debug.Log($"{name} Physical Hit!");
            return true;
        }*/
    }
}
