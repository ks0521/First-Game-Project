using UnityEngine;
using _Kamikakushi.Utills;

namespace _Kamikakushi.Contents.Monster
{
    public class MentalMonster : Monster
    {
        public override void Move(Vector3 targetPos)
        {
            Vector3 dir = (targetPos - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;

            // 방향 회전 추가 가능
            if (dir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5);
            }
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
        }
        /*public virtual bool Hit(Vector3 targetPos)
        {
            // 멘탈 타입 공격 처리
            Debug.Log($"{name} Mental Attack!");
            return true;
        }*/
    }
}
