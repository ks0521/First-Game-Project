using UnityEngine;
using UnityEngine.AI;
using System;

namespace _Kamikakushi.Contents.Monster
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] protected Detector detector;

        protected float speed = 3.5f;

        [Header("Movement Type")]
        [SerializeField] protected MovementType movementType = MovementType.NavMesh;

        protected NavMeshAgent agent;
        protected Vector3 startPos;
        protected Vector3 currentTargetPos;
        protected bool isChasing = false;

        private float lostTimer = 0f;
        [SerializeField] private float lostDelay = 3f;
        protected Animator animator;

        public event Action<Monster> OnChaseStarted; //델리게이트

        public abstract void Move(Vector3 targetPos);

        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            startPos = transform.position;

            if (movementType == MovementType.NavMesh)
            {
                agent = GetComponent<NavMeshAgent>();
                if (agent != null)
                    agent.speed = speed;
            }

            detector?.Init(this);
        }

        public virtual void OnPlayerDetected(Vector3 targetPos)
        {
            currentTargetPos = targetPos;
            lostTimer = 0f;

            if (!isChasing)
            {
                isChasing = true;
                OnChaseStarted?.Invoke(this);//델리게이트 발행
            }
        }

        public virtual void OnPlayerLost()
        {
            isChasing = false;
        }

        protected virtual void Update()
        {
            float moveSpeed = 0f;

            if (agent != null)
                moveSpeed = agent.velocity.magnitude;
            else
                moveSpeed = isChasing ? speed : 0f;

            animator?.SetFloat("Speed", moveSpeed);

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

        protected void MoveTo(Vector3 targetPos)
        {
            Move(targetPos);
        }
        public virtual void ForceStopChase()
        {
            isChasing = false;

            if (agent != null)
                agent.ResetPath();
        }
        public virtual void OnTouchedPlayer()
        {
            // 기본 동작: 아무것도 안 함
        }
        public virtual void OnRespawned()
        {
            // 추적 상태 초기화
            isChasing = false;
            currentTargetPos = startPos;

            // 이동 완전 정지
            if (agent != null)
            {
                agent.ResetPath();
                agent.velocity = Vector3.zero;
            }
        }


    }
}
