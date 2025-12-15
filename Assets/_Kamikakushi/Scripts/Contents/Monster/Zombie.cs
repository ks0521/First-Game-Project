using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class Zombie : PhysicalMonster
    {
        [Header("Zombie View Settings")]
        [SerializeField] private float viewAngle = 30f; // 시선 허용 각도
        [SerializeField] private float viewDistance = 10f;

        private Transform playerCam;

        protected override void Awake()
        {
            movementType = MovementType.NavMesh;
            speed = 2.5f;

            base.Awake();

            // 메인 카메라 = 플레이어 시선
            playerCam = Camera.main.transform;
        }

        protected override void Update()
        {
            if (isChasing)
            {
                if (IsPlayerLookingAtZombie())
                {
                    StopChase();
                    return;
                }
            }

            base.Update();
        }

        private bool IsPlayerLookingAtZombie()
        {
            if (playerCam == null) return false;

            Vector3 toZombie = (transform.position - playerCam.position).normalized;

            // 1️⃣ 각도 체크
            float angle = Vector3.Angle(playerCam.forward, toZombie);
            if (angle > viewAngle) return false;

            // 2️⃣ 거리 체크
            float distance = Vector3.Distance(playerCam.position, transform.position);
            if (distance > viewDistance) return false;

            // 3️⃣ Raycast (벽 뒤에 있는지 체크)
            if (Physics.Raycast(playerCam.position, toZombie, out RaycastHit hit, viewDistance))
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