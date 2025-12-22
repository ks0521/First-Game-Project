using UnityEngine;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Enums;

namespace _Kamikakushi.Contents.Monster
{
    public class PhysicalMonster : Monster
    {
        public override void Move(Vector3 targetPos)
        {
            if (agent == null) return;
            agent.SetDestination(targetPos);
        }
        IHittable hittable;
        IDetectable detectable;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("트리거 충돌");
            //대상이 IHittable이고 
            if (other.TryGetComponent<IHittable>(out hittable))
            {
                detectable = other.GetComponent<IDetectable>();
                //플레이어가 CanDetected인 경우(숨어있지 않은 경우)
                if (detectable != null && detectable.CanDetected)
                    Debug.Log("플레이어 충돌");
                hittable.Hit(transform.position, 30, 2, HitType.Physical);
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
