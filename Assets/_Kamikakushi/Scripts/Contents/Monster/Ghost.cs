using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    //보통의 속도로 추적하는 멘탈몬스터
    public class Ghost : MentalMonster
    {
        [Header("Ghost Extra Rule")]
        [SerializeField] private float pauseWhenHiddenTime = 2f;

        private float pauseTimer = 0f;
        private bool isPausedByHide = false;

        private Transform player;
        private Detectorble playerDetectorble;

        protected override void Awake()
        {
            speed = 3.8f;   // Zombie / Banshee처럼 NavMesh 이동
            base.Awake();

            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            playerDetectorble = player?.GetComponent<Detectorble>();
        }

        // ❌ Update 오버라이드 금지
        // ✅ Monster.OnMonsterUpdate만 사용
        protected override void OnMonsterUpdate()
        {
            // 숨었을 때 잠깐 멈춤 (Ghost 전용 연출)
            if (isPausedByHide)
            {
                pauseTimer += Time.deltaTime;
                if (pauseTimer >= pauseWhenHiddenTime)
                {
                    isPausedByHide = false;
                    pauseTimer = 0f;
                }
                return;
            }

            // 공통 추적 / 복귀 로직
            base.OnMonsterUpdate();
        }

        public override void OnPlayerDetected(Vector3 targetPos)
        {
            if (playerDetectorble != null && !playerDetectorble.CanDetected)
            {
                // 👻 Ghost 연출: 숨으면 잠깐 멈춤
                PauseByHide();
                return;
            }

            base.OnPlayerDetected(targetPos);
        }

        private void PauseByHide()
        {
            isPausedByHide = true;
            pauseTimer = 0f;

            if (agent != null)
            {
                agent.ResetPath();
                agent.isStopped = true;
            }

            animator?.SetFloat("Speed", 0f);
        }

        public virtual void OnRespawned()
        {
            isTouchingPlayer = false;
            StopAll();

            // ⭐ NavMesh 몬스터
            if (agent != null)
            {
                agent.Warp(startPos);
            }
            // ⭐ MentalMonster (Transform 이동)
            else
            {
                transform.position = startPos;
            }

            detector?.SetEnable(true);
            animator?.SetFloat("Speed", 0f);
        }
    }
}
