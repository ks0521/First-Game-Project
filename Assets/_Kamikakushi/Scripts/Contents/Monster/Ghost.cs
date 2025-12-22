using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

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

            ForceStopAndIdle();
        }

        // ❌ Update() 제거
        // ✅ 이 함수만 오버라이드
        protected override void OnMonsterUpdate()
        {
            // 🔴 최대 추적 거리 제한
            if (state != GhostState.Idle)
            {
                float dist = Vector3.Distance(transform.position, player.position);
                if (dist >= maxChaseDistance)
                {
                    ForceStopAndIdle();
                    return;
                }
            }

            switch (state)
            {
                case GhostState.Idle:
                    // 아무 것도 안 함
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
            WakeUpAnimator();

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

            animator?.SetFloat("Speed", 0f);
        }

        // =========================
        // NavMesh 안정화
        // =========================

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
            agent.Warp(transform.position);
        }

        private void WakeUpAnimator()
        {
            animator?.SetFloat("Speed", 0.2f);
        }

        public override void OnRespawned()
        {
            base.OnRespawned();
            ForceStopAndIdle();
        }
    }
}
