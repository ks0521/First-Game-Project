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

        protected override void Update()
        {
            if (IsInFreezeRange() && IsPlayerLookingAtZombie())
            {
                Freeze();
                return;
            }

            isFrozenByLook = false;
            base.Update();
        }

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

        private void Freeze()
        {
            if (isFrozenByLook) return;

            isFrozenByLook = true;
            isChasing = false;

            if (agent != null)
                agent.ResetPath();
        }

        public override void OnPlayerDetected(Vector3 targetPos)
        {
            if (isFrozenByLook)
                return;

            base.OnPlayerDetected(targetPos);
        }

    }
}
