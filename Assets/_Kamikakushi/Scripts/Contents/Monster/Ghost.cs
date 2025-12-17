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
}


namespace _Kamikakushi.Contents.Monster
{
    public class Ghost : PhysicalMonster
    {
        [Header("Ghost Settings")]
        [SerializeField] private float fakeReturnTime = 2.5f;
        [SerializeField] private float surpriseChaseTime = 5f;

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

            // 돌아가는 척
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
                state = GhostState.Idle;
                isChasing = false;
            }
        }

        private void EnterFakeReturn()
        {
            state = GhostState.FakeReturn;
            stateTimer = 0f;
            isChasing = false;

            if (agent != null)
                agent.ResetPath();
        }

        private void EnterSurpriseChase()
        {
            state = GhostState.SurpriseChase;
            stateTimer = 0f;
            isChasing = true;
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

            // 🔥 Ghost 전용 상태 초기화
            state = GhostState.Idle;
            stateTimer = 0f;

            // NavMesh 완전 정지
            if (agent != null)
            {
                agent.ResetPath();
                agent.velocity = Vector3.zero;
            }
        }

    }
}
