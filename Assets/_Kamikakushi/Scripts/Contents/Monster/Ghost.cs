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
        }

        protected override void Update()
        {
            // 🔥 거리 초과 시 즉시 추적 중단 (모든 상태 공통)
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
                    base.Update();
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

        private void ChaseUpdate()
        {
            MoveTo(player.position);

            if (playerDetectorble != null && !playerDetectorble.CanDetected)
            {
                EnterFakeReturn();
            }
        }

        private void FakeReturnUpdate()
        {
            stateTimer += Time.deltaTime;
            MoveTo(startPos);

            if (stateTimer >= fakeReturnTime)
            {
                EnterSurpriseChase();
            }
        }

        private void SurpriseChaseUpdate()
        {
            stateTimer += Time.deltaTime;
            MoveTo(player.position);

            if (stateTimer >= surpriseChaseTime)
            {
                ForceStopAndIdle();
            }
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
            state = GhostState.SurpriseChase;
            stateTimer = 0f;
            isChasing = true;
        }

        private void ForceStopAndIdle()
        {
            state = GhostState.Idle;
            stateTimer = 0f;
            isChasing = false;

            agent?.ResetPath();
            agent.velocity = Vector3.zero;
        }

        public override void OnPlayerDetected(Vector3 targetPos)
        {
            if (state != GhostState.Idle)
                return;

            state = GhostState.Chasing;
            isChasing = true;
        }

        public override void OnRespawned()
        {
            base.OnRespawned();
            ForceStopAndIdle();
        }
    }
}
