using UnityEngine;
using UnityEngine.AI;

namespace _Kamikakushi.Contents.Monster
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("Monster Base Settings")]
        [SerializeField] protected Detector detector;
        [SerializeField] protected float speed = 3.5f;

        [Header("Tracking Settings")]
        [SerializeField] protected float detachDistance = 10f; // 플레이어와 이 거리 이상 벌어지면 추적 종료

        protected Vector3 startPos;
        protected Vector3 currentTargetPos;

        protected NavMeshAgent agent;
        private Transform player; // 플레이어 Transform 저장

        protected virtual void Awake()
        {
            startPos = transform.position;

            agent = GetComponent<NavMeshAgent>();
            if (agent != null)
                agent.speed = speed;

            if (detector != null)
                detector.Init(this);

            // Player 찾기 (tag가 Player여야 함)
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        public abstract void Move(Vector3 targetPos);

        public virtual void OnPlayerDetected(Vector3 playerPos)
        {
            currentTargetPos = playerPos;
            Move(currentTargetPos);
        }

        protected virtual bool SearchPlayer()
        {
            if (player == null) return false;

            float distance = Vector3.Distance(transform.position, player.position);

            // 플레이어와의 거리가 특정 범위를 벗어나면 false 반환
            return distance <= detachDistance;
        }

        protected virtual void ReturnToStart()
        {
            Move(startPos);
        }

        private void Update()
        {
            // 플레이어 추적 상태에서 기준 거리 벗어나면 복귀
            if (!SearchPlayer())
            {
                ReturnToStart();
            }
        }
    }
}
