using _Kamikakushi.Utills.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace _Kamikakushi.Contents.Monster
{
    public enum GhostState
    {
        Idle,
        Chasing,
        FakeReturn,
        SurpriseChase
    }

    public class Ghost : PhysicalMonster
    {
        [Header("Ghost Settings")]
        [SerializeField] private float fakeReturnTime = 2.5f;
        [SerializeField] private float surpriseChaseTime = 5f;

        [Header("Chase Limit")]
        [SerializeField] private float maxChaseDistance = 15f;

        private GhostState state = GhostState.Idle;
        private float stateTimer = 0f;

        private Transform player;
        private Detectorble playerDetectorble;

        protected override void Awake()
        {
            movementType = MovementType.NavMesh;
            speed = 3.8f;
            base.Awake();

            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerDetectorble = player.GetComponent<Detectorble>();

            ForceStopAndIdle(); // ⭐ 시작 상태 완전 초기화
        }

        protected override void Update()
        {
            // 🔴 최대 추적 거리 초과 시 즉시 Idle
            if (state != GhostState.Idle)
            {
                float dist = Vector3.Distance(transform.position, player.position);
                if (dist >= maxChaseDistance)
                {
                    ForceStopAndIdle();
                    UpdateAnimatorSpeed();
                    return;
                }
            }

            switch (state)
            {
                case GhostState.Idle:
                    base.Update(); // Monster.Update 실행
                    break;

                case GhostState.Chasing:
                    ChaseUpdate();
                    break;

                case GhostState.FakeReturn:
                    FakeReturnUpdate();
                    break;

                case GhostState.SurpriseChase:
                    SurpriseChaseUpdate();
                    break;
            }

            // ⭐⭐⭐ 애니메이션 Speed는 항상 직접 갱신
            UpdateAnimatorSpeed();
        }

        // =========================
        // 상태별 Update
        // =========================

        private void ChaseUpdate()
        {
            EnsureAgentMove();
            isChasing = true;

            MoveTo(player.position);

            if (playerDetectorble != null && !playerDetectorble.CanDetected)
                EnterFakeReturn();
        }

        private void FakeReturnUpdate()
        {
            EnsureAgentMove();
            isChasing = false;

            stateTimer += Time.deltaTime;
            MoveTo(startPos);

            if (stateTimer >= fakeReturnTime)
                EnterSurpriseChase();
        }

        private void SurpriseChaseUpdate()
        {
            EnsureAgentMove();
            isChasing = true;

            stateTimer += Time.deltaTime;
            MoveTo(player.position);

            if (stateTimer >= surpriseChaseTime)
                ForceStopAndIdle();
        }

        // =========================
        // 상태 전환
        // =========================

        public override void OnPlayerDetected(Vector3 targetPos)
        {
            if (state != GhostState.Idle)
                return;

            ResetAgentCompletely();
            WakeUpAnimator();   // ⭐ 핵심

            state = GhostState.Chasing;
            isChasing = true;
        }

        private void EnterFakeReturn()
        {
            state = GhostState.FakeReturn;
            stateTimer = 0f;
            isChasing = false;

            agent?.ResetPath();
        }

        private void EnterSurpriseChase()
        {
            ResetAgentCompletely();
            state = GhostState.SurpriseChase;
            stateTimer = 0f;
            isChasing = true;
        }

        private void ForceStopAndIdle()
        {
            state = GhostState.Idle;
            stateTimer = 0f;
            isChasing = false;

            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
                agent.velocity = Vector3.zero;
            }

            UpdateAnimatorSpeed();
        }

        // =========================
        // Animator & NavMesh 안정화
        // =========================

        private void UpdateAnimatorSpeed()
        {
            if (animator == null || agent == null) return;

            float moveSpeed = 0f;
            if (agent.remainingDistance > agent.stoppingDistance)
                moveSpeed = speed;

            animator.SetFloat("Speed", moveSpeed);
        }
        private void EnsureAgentMove()
        {
            if (agent != null && agent.isStopped)
                agent.isStopped = false;
        }

        private void ResetAgentCompletely()
        {
            if (agent == null) return;

            agent.isStopped = false;
            agent.ResetPath();
            agent.Warp(transform.position); // ⭐ 내부 상태 완전 리셋
        }

        public override void OnRespawned()
        {
            base.OnRespawned();
            ForceStopAndIdle();
        }
        private void WakeUpAnimator()
        {
            if (animator == null) return;

            // Idle → Move 전이 강제
            animator.SetFloat("Speed", 0.2f);
        }
    }
}
