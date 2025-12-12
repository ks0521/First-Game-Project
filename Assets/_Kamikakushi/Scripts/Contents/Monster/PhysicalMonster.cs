using UnityEngine;
using UnityEngine.AI;

namespace Assets._Kamikakushi.Contents.Monster
{
    public class PhysicalMonster : Monster
    {
        private NavMeshAgent agent;              // 이동을 담당하는 NavMeshAgent
        private Transform trackingTarget;        // 현재 추적 중인 대상 (플레이어)

        private Vector3 startPosition;           // 몬스터의 초기 위치(복귀 지점)

        public float maxChaseDistance = 15f;     // 추적 중 유지 가능한 최대 거리
        public float detectRange = 8f;           // Idle 상태에서 플레이어를 감지하는 거리
        public float returnStopDistance = 0.5f;  // 복귀 시 도착했다고 판단하는 거리

        private bool isReturning = false;        // 현재 '복귀 중'인지 여부
        private bool isIdle = true;              // 플레이어를 감지하기 전의 대기 상태

        public Transform player;                 // 추적 대상 (플레이어 Transform)

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();  // NavMeshAgent 가져오기
            startPosition = transform.position;     // 초기 위치 저장
        }

        private void Update()
        {
            // ---------------------------------------------
            // 플레이어와의 거리 계산 (플레이어가 없으면 무한대)
            // ---------------------------------------------
            float distanceToPlayer = (player != null)
                ? Vector3.Distance(transform.position, player.position)
                : Mathf.Infinity;

            // -----------------------------------------------------
            // 1) Idle 상태 → 플레이어가 감지 거리 안에 들어오면 추적 시작
            // -----------------------------------------------------
            if (isIdle && distanceToPlayer <= detectRange)
            {
                StartTracking(player);
                return;
            }

            // -----------------------------------------------------
            // 2) 추적 중 상태
            // -----------------------------------------------------
            if (trackingTarget != null && !isReturning)
            {
                float dist = Vector3.Distance(transform.position, trackingTarget.position);

                // 추적 중 대상이 너무 멀어지면 복귀 전환
                if (dist > maxChaseDistance)
                {
                    StartReturn();
                    return;
                }

                // 계속 추적 (NavMeshAgent가 경로 계산)
                agent.SetDestination(trackingTarget.position);
                return;
            }

            // -----------------------------------------------------
            // 3) 복귀 중 상태
            // -----------------------------------------------------
            if (isReturning)
            {
                // StartPosition으로 복귀
                agent.SetDestination(startPosition);

                // 경로 계산이 끝났으며, 목표 지점에 거의 도착한 경우
                if (!agent.pathPending && agent.remainingDistance <= returnStopDistance)
                {
                    StopToIdle();  // Idle 상태로 전환
                }
            }
        }

        // ===========================================================
        // 추적 시작 로직
        // ===========================================================
        private void StartTracking(Transform target)
        {
            trackingTarget = target;   // 추적 대상 설정
            isReturning = false;
            isIdle = false;

            agent.isStopped = false;   // 이동 재개
            agent.SetDestination(target.position);
        }

        // ===========================================================
        // 강제로 특정 위치로 이동시키는 Monster 기본 기능 (부모 override)
        // ===========================================================
        public override void Move(Vector3 targetPos)
        {
            trackingTarget = null;
            isReturning = false;
            isIdle = false;

            agent.isStopped = false;
            agent.SetDestination(targetPos);
        }

        // ===========================================================
        // 복귀 시작
        // ===========================================================
        private void StartReturn()
        {
            trackingTarget = null;  // 추적 대상 제거
            isReturning = true;
            isIdle = false;

            agent.isStopped = false;   // 이동 활성화
        }

        // ===========================================================
        // 복귀 완료 후 Idle 상태로 전환
        // ===========================================================
        private void StopToIdle()
        {
            isReturning = false;
            isIdle = true;

            agent.isStopped = true;    // 완전히 정지
        }

        // ===========================================================
        // 외부에서 이동/추적을 멈출 필요가 있을 때 호출
        // ===========================================================
        public void StopMove()
        {
            trackingTarget = null;
            isReturning = false;
            isIdle = true;

            agent.isStopped = true;    // NavMeshAgent 정지
        }
    }
}
