using UnityEngine;
using UnityEngine.AI;

namespace _Kamikakushi.Contents.Monster
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] protected Detector detector;
        [SerializeField] protected float speed = 3.5f;

        [Header("Chase / Return")]
        [SerializeField] protected float giveUpPlayerDistance = 15f; // 플레이어와 거리
        [SerializeField] protected float waitBeforeReturnTime = 3f;  // ⭐ 멈추는 시간
        [SerializeField] protected float returnStopDistance = 0.5f;

        protected NavMeshAgent agent;
        protected Animator animator;

        protected Vector3 startPos;
        protected Vector3 currentTargetPos;
        protected Transform player;

        protected bool isChasing = false;
        protected bool isWaitingToReturn = false; // ⭐ 추가
        protected bool isReturning = false;
        protected bool isTouchingPlayer = false;

        private float waitTimer = 0f;

        public bool IsTouchingPlayer => isTouchingPlayer;

        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            startPos = transform.position;

            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            agent = GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.speed = speed;
                agent.isStopped = true;
            }

            detector?.Init(this);
        }

        // Update 봉인
        private void Update()
        {
            if (isTouchingPlayer)
                return;

            OnMonsterUpdate();
            UpdateAnimator();
        }

        // =========================
        // 공통 상태 로직
        // =========================
        protected virtual void OnMonsterUpdate()
        {
            // 🔴 추적 중
            if (isChasing)
            {
                if (player != null)
                {
                    float dist = Vector3.Distance(transform.position, player.position);

                    if (dist >= giveUpPlayerDistance)
                    {
                        StartWaitBeforeReturn();
                        return;
                    }
                }

                MoveTo(currentTargetPos);
                return;
            }

            // 🟡 멈춰서 대기 중
            if (isWaitingToReturn)
            {
                waitTimer += Time.deltaTime;

                if (waitTimer >= waitBeforeReturnTime)
                {
                    StartReturn();
                }
                return;
            }

            // 🔵 복귀 중
            if (isReturning)
            {
                MoveTo(startPos);

                if (Vector3.Distance(transform.position, startPos)
                    <= returnStopDistance)
                {
                    StopAll();
                }
            }
        }

        // =========================
        // 이동
        // =========================
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

            isChasing = true;
            isWaitingToReturn = false;
            isReturning = false;

            waitTimer = 0f;

            if (agent != null)
                agent.isStopped = false;
        }

        // =========================
        // 상태 전환
        // =========================
        protected void StartWaitBeforeReturn()
        {
            isChasing = false;
            isWaitingToReturn = true;
            isReturning = false;

            waitTimer = 0f;

            if (agent != null)
            {
                agent.ResetPath();
                agent.isStopped = true; // ⭐ 완전 정지
            }
        }

        protected void StartReturn()
        {
            isWaitingToReturn = false;
            isReturning = true;

            if (agent != null)
            {
                agent.isStopped = false;
                agent.ResetPath();
            }
        }

        protected void StopAll()
        {
            isChasing = false;
            isWaitingToReturn = false;
            isReturning = false;

            waitTimer = 0f;

            if (agent != null)
            {
                agent.ResetPath();
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
            }
        }

        // =========================
        // 접촉 / 리스폰
        // =========================
        public virtual void OnTouchedPlayer()
        {
            isTouchingPlayer = true;
            StopAll();

            detector?.SetEnable(false);
            animator?.SetFloat("Speed", 0f);
        }

        public virtual void OnRespawned()
        {
            isTouchingPlayer = false;
            StopAll();

            if (agent != null)
                agent.Warp(startPos);
            else
                transform.position = startPos;

            detector?.SetEnable(true);
            animator?.SetFloat("Speed", 0f);
        }

        // =========================
        // Animator (논리 기준)
        // =========================
        protected virtual void UpdateAnimator()
        {
            if (animator == null) return;

            float animSpeed = 0f;

            if (isChasing || isReturning)
                animSpeed = speed;

            animator.SetFloat("Speed", animSpeed);
        }
    }
}
