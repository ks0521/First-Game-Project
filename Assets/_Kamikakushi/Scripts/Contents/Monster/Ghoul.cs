using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class Ghoul : PhysicalMonster
    {
        [Header("Ghoul Rule")]
        [SerializeField] private bool neverGiveUp = true;

        protected override void Awake()
        {
            speed = 3.2f;
            base.Awake();
        }

        // ❌ Monster FSM을 우회
        protected override void OnMonsterUpdate()
        {
            // 플레이어를 한 번도 못 찾았으면 아무 것도 안 함
            if (!isChasing)
                return;

            // ⭐ 끝까지 추적
            if (player != null)
            {
                currentTargetPos = player.position;
                MoveTo(currentTargetPos);
            }
        }

        // =========================
        // 감지
        // =========================
        public override void OnPlayerDetected(Vector3 targetPos)
        {
            if (isTouchingPlayer)
                return;

            // ⭐ 최초 발견 시 바로 추적 시작
            isChasing = true;
            isReturning = false;

            currentTargetPos = targetPos;

            if (agent != null)
                agent.isStopped = false;
        }

        // =========================
        // 복귀 / 대기 무력화
        // =========================
        

        public override void OnRespawned()
        {
            // 리스폰되면 다시 처음 상태
            isTouchingPlayer = false;
            isChasing = false;

            if (agent != null)
            {
                agent.Warp(startPos);
                agent.isStopped = true;
            }

            animator?.SetFloat("Speed", 0f);
        }
    }
}
