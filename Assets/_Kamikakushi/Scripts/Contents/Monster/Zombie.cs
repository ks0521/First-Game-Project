using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class Zombie : PhysicalMonster
    {
        [Header("Zombie Rules")]
        [SerializeField] private float freezeDistance = 15f;
        [SerializeField] private float viewAngle = 30f;

        private Transform playerCam;
        private bool isFrozenByLook = false;

        protected override void Awake()
        {
            movementType = MovementType.NavMesh;
            speed = 2.5f;
            base.Awake();

            playerCam = Camera.main.transform;
        }

        // ❌ Update() 제거
        // ✅ 이 함수만 오버라이드
        protected override void OnMonsterUpdate()
        {
            if (IsInFreezeRange() && IsPlayerLookingAtZombie())
            {
                Freeze();
                return;
            }

            if (isFrozenByLook)
            {
                UnFreeze();
            }

            // 🔥 기본 추적 로직 실행
            base.OnMonsterUpdate();
        }

        // =========================
        // Freeze Logic
        // =========================
        private void Freeze()
        {
            if (isFrozenByLook) return;

            isFrozenByLook = true;
            isChasing = false;

            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }

            animator?.SetFloat("Speed", 0f);
        }

        private void UnFreeze()
        {
            isFrozenByLook = false;

            if (agent != null)
                agent.isStopped = false;
        }

        // =========================
        // Look Check
        // =========================
        private bool IsInFreezeRange()
        {
            float dist = Vector3.Distance(playerCam.position, transform.position);
            return dist <= freezeDistance;
        }

        private bool IsPlayerLookingAtZombie()
        {
            Vector3 toZombie = (transform.position - playerCam.position).normalized;
            float angle = Vector3.Angle(playerCam.forward, toZombie);

            if (angle > viewAngle) return false;

            if (Physics.Raycast(
                playerCam.position,
                toZombie,
                out RaycastHit hit,
                freezeDistance))
            {
                return hit.transform == transform;
            }

            return false;
        }

        public override void OnPlayerDetected(Vector3 targetPos)
        {
            if (isFrozenByLook)
                return;

            base.OnPlayerDetected(targetPos);
        }
    }
}
