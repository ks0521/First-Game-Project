using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class Zombie : PhysicalMonster
    {
        [Header("Zombie View Settings")]
        [SerializeField] private float viewAngle = 30f; // 시야 각도

        private Transform playerCam;

        protected override void Awake()
        {
            movementType = MovementType.NavMesh;
            speed = 2.5f;
            base.Awake();

            playerCam = Camera.main.transform;
        }

        protected override void Update()
        {
            if (isChasing && IsPlayerLookingAtZombie())
            {
                StopChase();
                return;
            }

            base.Update();
        }

        private bool IsPlayerLookingAtZombie()
        {
            if (playerCam == null) return false;

            Vector3 toZombie = (transform.position - playerCam.position).normalized;

            // 1️⃣ 각도 체크 (거리 무관)
            float angle = Vector3.Angle(playerCam.forward, toZombie);
            if (angle > viewAngle) return false;

            // 2️⃣ Raycast (무한 거리)
            if (Physics.Raycast(
                playerCam.position,
                toZombie,
                out RaycastHit hit,
                Mathf.Infinity))
            {
                if (hit.transform == transform)
                    return true;
            }

            return false;
        }

        private void StopChase()
        {
            isChasing = false;

            if (agent != null)
                agent.ResetPath();
        }
    }
}
