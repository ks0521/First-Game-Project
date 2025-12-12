using Assets._Kamikakushi.Contents.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Assets._Kamikakushi.Contents.Monster
{
    public abstract class Monster : MonoBehaviour
    {
        [SerializeField] protected Detector detector;
        protected Vector3 startPos;
        protected Vector3 currentTargetPos;
        protected float speed;

        protected virtual void Awake()
        {
            //몬스터 생성 시 자신의 위치, 디텍터 초기화
            startPos = transform.position;
            if (detector != null)
                detector.Init(this);
        }

        public abstract void Move(Vector3 targetPos);

        public virtual void OnPlayerDetected(Vector3 playerPos)
        {
            //플레이어 탐지 성공시 실행하는 함수
            currentTargetPos = playerPos;
            //탐지된 플레이어의 위치로 이동
            Move(currentTargetPos);

            //만약 플레이어를 탐색하지 못했다면
            if (!SearchPlayer())
            {
                //원래 위치로 복귀
                ReturnToStart();
            }
        }

        protected virtual bool SearchPlayer()
        {
            // 시야 / 거리 체크 등
            return false;
        }

        protected virtual void ReturnToStart()
        {
            //원래 위치로 이동
            Move(startPos);
        }
    }
}
