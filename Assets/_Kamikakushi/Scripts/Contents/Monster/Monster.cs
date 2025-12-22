using UnityEngine;
using UnityEngine.AI;

namespace _Kamikakushi.Contents.Monster
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] protected Detector detector;
        [SerializeField] protected float speed = 3.5f;
        [SerializeField] protected MovementType movementType = MovementType.NavMesh;

        protected NavMeshAgent agent;
        protected Animator animator;

        protected Vector3 startPos;
        protected Vector3 currentTargetPos;

        protected bool isChasing = false;

        // 🔒 AI 완전 잠금 플래그
        protected bool isTouchingPlayer = false;
        public bool IsTouchingPlayer => isTouchingPlayer;

        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            startPos = transform.position;

            if (movementType == MovementType.NavMesh)
            {
                agent = GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.speed = speed;
                    agent.isStopped = true;
                }
            }

            detector?.Init(this);
        }

        // 🔥 절대 자식이 Override 하면 안 되는 Update
        private void Update()
        {
            // 플레이어와 닿은 이후엔 AI 완전 정지
            if (isTouchingPlayer)
                return;

            OnMonsterUpdate();
            UpdateAnimator();
        }

        // =========================
        // 자식 전용 Update
        // =========================
        protected virtual void OnMonsterUpdate()
        {
            if (isChasing)
            {
                MoveTo(currentTargetPos);
            }
        }

        protected abstract void Move(Vector3 targetPos);

        protected void MoveTo(Vector3 targetPos)
        {
            Move(targetPos);
        }

        // =========================
        // 감지
        // =========================
        public virtual void OnPlayerDetected(Vector3 targetPos)
        {
            if (isTouchingPlayer) return;

            currentTargetPos = targetPos;

            if (!isChasing)
            {
                isChasing = true;
                if (agent != null)
                    agent.isStopped = false;
            }
        }

        // =========================
        // 접촉 시 (모든 몬스터 공통)
        // =========================
        public virtual void OnTouchedPlayer()
        {
            isTouchingPlayer = true;
            isChasing = false;

            detector?.SetEnable(false);

            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
                agent.velocity = Vector3.zero;
            }

            animator?.SetFloat("Speed", 0f);
        }

        // =========================
        // 리스폰
        // =========================
        public virtual void OnRespawned()
        {
            isTouchingPlayer = false;
            isChasing = false;

            detector?.SetEnable(true);

            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
                agent.Warp(startPos);
            }

            animator?.SetFloat("Speed", 0f);
        }

        // =========================
        // Animator
        // =========================
        protected virtual void UpdateAnimator()
        {
            if (animator == null || agent == null) return;

            float animSpeed =
                (!agent.isStopped && agent.hasPath) ? speed : 0f;

            animator.SetFloat("Speed", animSpeed);
        }
    }
}
