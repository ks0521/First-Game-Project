using UnityEngine;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Enums;
using UnityEngine.UIElements;

namespace _Kamikakushi.Contents.Monster
{
    public class PhysicalMonster : Monster
    {

        // 🔥 public → protected
        protected override void Move(Vector3 targetPos)
        {
            if (agent == null) return;
            agent.SetDestination(targetPos);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("트리거 충돌");

            if (!other.TryGetComponent<IDetectable>(out var detectable))
                return;

            if (!detectable.CanDetected)
                return;

            if (other.TryGetComponent<IHittable>(out var hittable))
            {
                Debug.Log("플레이어 충돌");
                hittable.Hit(hitpos.position, damage, 2, HitType.Physical);
            }
        }
    }
}
