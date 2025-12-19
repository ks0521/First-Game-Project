using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;
using UnityEngine.UI;

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
                hittable.Hit(transform.position, 5, 2, HitType.Mental);
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
