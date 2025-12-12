using UnityEngine;
using UnityEngine.AI;

namespace _Kamikakushi.Contents.Monster
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("Monster Base Settings")]
        [SerializeField] protected Detector detector;
        [SerializeField] protected float speed = 3.5f;

        protected Vector3 startPos;
        protected Vector3 currentTargetPos;

        protected NavMeshAgent agent;

        protected virtual void Awake()
        {
            startPos = transform.position;

            agent = GetComponent<NavMeshAgent>();
            if (agent != null)
                agent.speed = speed;

            if (detector != null)
                detector.Init(this);
        }

        public abstract void Move(Vector3 targetPos);

        public virtual void OnPlayerDetected(Vector3 playerPos)
        {
            currentTargetPos = playerPos;
            Move(currentTargetPos);

            if (!SearchPlayer())
            {
                ReturnToStart();
            }
        }

        protected virtual bool SearchPlayer()
        {
            // 시야 / 거리 체크 등 구현 가능
            return true;
        }

        protected virtual void ReturnToStart()
        {
            Move(startPos);
        }
    }
}
