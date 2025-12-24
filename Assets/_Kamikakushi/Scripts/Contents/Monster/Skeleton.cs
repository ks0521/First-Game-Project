using UnityEngine;
using UnityEngine.AI;

namespace _Kamikakushi.Contents.Monster
{
    public class Skeleton : PhysicalMonster
    {
        [Header("Waypoint Patrol")]
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float waitTimeAtPoint = 2f;
        [SerializeField] private bool randomOrder = false;

        private int currentIndex = 0;
        private float waitTimer = 0f;

        protected override void Awake()
        {
            speed = 2.8f;
            base.Awake();
        }

        protected override void OnMonsterUpdate()
        {
            // 🔴 추적 중이면 공통 로직 사용
            if (isChasing)
            {
                base.OnMonsterUpdate();
                return;
            }

            PatrolUpdate();
        }

        // =========================
        // Waypoint Patrol
        // =========================
        private void PatrolUpdate()
        {
            if (waypoints == null || waypoints.Length == 0)
                return;

            if (agent.isStopped)
                agent.isStopped = false;

            // 목적지 설정 안 돼 있으면 설정
            if (!agent.hasPath)
            {
                agent.SetDestination(waypoints[currentIndex].position);
            }

            // 도착 체크
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                waitTimer += Time.deltaTime;

                if (waitTimer >= waitTimeAtPoint)
                {
                    waitTimer = 0f;
                    SelectNextWaypoint();
                }
            }
        }

        private void SelectNextWaypoint()
        {
            if (randomOrder)
            {
                currentIndex = Random.Range(0, waypoints.Length);
            }
            else
            {
                currentIndex++;
                if (currentIndex >= waypoints.Length)
                    currentIndex = 0;
            }

            agent.ResetPath();
        }

        // =========================
        // 감지
        // =========================
        public override void OnPlayerDetected(Vector3 targetPos)
        {
            agent.ResetPath();
            base.OnPlayerDetected(targetPos);
        }

        public override void OnRespawned()
        {
            base.OnRespawned();

            currentIndex = 0;
            waitTimer = 0f;
        }
    }
}
