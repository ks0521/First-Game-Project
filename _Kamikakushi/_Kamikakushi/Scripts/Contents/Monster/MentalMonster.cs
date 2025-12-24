using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class MentalMonster : Monster
    {
        // 🔥 public → protected 로 변경
        protected override void Move(Vector3 targetPos)
        {
            Vector3 dir = (targetPos - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;

            if (dir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    rot,
                    Time.deltaTime * 5
                );
            }
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
                hittable.Hit(transform.position, 5, 2, HitType.Mental);
            }
        }
    }
}
