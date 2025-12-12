using UnityEngine;
using UnityEngine.AI;

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
        protected Vector3 startPos;
        protected Vector3 currentTargetPos;
        protected bool isChasing = false;

        private float lostTimer = 0f;
        [SerializeField] private float lostDelay = 3f;

        public abstract void Move(Vector3 targetPos);
        protected virtual void Awake()
        {
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
            isChasing = true;
            lostTimer = 0f;
            currentTargetPos = targetPos;
        }

        public virtual void OnPlayerLost()
        {
            isChasing = false;
        }

        protected virtual void Update()
        {
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

        // 이동 방식 공통 API
        protected void MoveTo(Vector3 targetPos)
        {
            switch (movementType)
            {
                case MovementType.NavMesh:
                    Move(targetPos);
                    break;

                case MovementType.Transform:
                    Move(targetPos);
                    break;
            }
        }

        
    }
}
