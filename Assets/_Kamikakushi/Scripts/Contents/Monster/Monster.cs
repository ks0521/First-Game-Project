using UnityEngine;
using UnityEngine.AI;
using System;

namespace _Kamikakushi.Contents.Monster
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] protected Detector detector;

        [SerializeField] protected float speed = 3.5f;

        [Header("Movement Type")]
        [SerializeField] protected MovementType movementType = MovementType.NavMesh;

        protected NavMeshAgent agent;
        protected Animator animator;

        protected Vector3 startPos;
        protected Vector3 currentTargetPos;
        protected bool isChasing = false;

        private float lostTimer = 0f;
        [SerializeField] private float lostDelay = 3f;

        public event Action<Monster> OnChaseStarted;

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
                    agent.updateRotation = true;
                    agent.isStopped = true; // ⭐ 시작은 멈춘 상태
                }
            }

            detector?.Init(this);
        }

        protected virtual void Update()
        {
            UpdateAnimator();

            if (isChasing)
            {
                MoveTo(currentTargetPos);
            }
            else
            {
                lostTimer += Time.deltaTime;
                if (lostTimer >= lostDelay)
                    MoveTo(startPos);
            }
        }

        // =========================
        // Animator 제어 (핵심)
        // =========================
        protected virtual void UpdateAnimator()
        {
            float animSpeed = 0f;

            if (agent != null)
            {
                if (!agent.isStopped && agent.hasPath)
                    animSpeed = agent.speed;
            }
            else
            {
                animSpeed = isChasing ? speed : 0f;
            }

            animator?.SetFloat("Speed", animSpeed);
        }

        // =========================
        // Chase
        // =========================
        public virtual void OnPlayerDetected(Vector3 targetPos)
        {
            currentTargetPos = targetPos;
            lostTimer = 0f;

            if (!isChasing)
            {
                isChasing = true;
                agent?.SetDestination(targetPos);
                agent.isStopped = false;

                OnChaseStarted?.Invoke(this);
            }
        }

        public virtual void OnPlayerLost()
        {
            isChasing = false;
        }

        protected void MoveTo(Vector3 targetPos)
        {
            Move(targetPos);
        }

        public abstract void Move(Vector3 targetPos);

        // =========================
        // 강제 정지 (Freeze / Respawn용)
        // =========================
        public virtual void ForceStopChase()
        {
            isChasing = false;

            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }

            animator?.SetFloat("Speed", 0f);
        }

        public virtual void OnRespawned()
        {
            isChasing = false;
            currentTargetPos = startPos;

            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }

            animator?.SetFloat("Speed", 0f);
        }
    }
}
