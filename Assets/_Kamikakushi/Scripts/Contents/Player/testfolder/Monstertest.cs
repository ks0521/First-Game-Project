using UnityEngine;
using UnityEngine.AI;
using System;
using _Kamikakushi.Contents.Monster;
using _Kamikakushi.Utills.Interfaces;
public enum ChasingState
{
    Idle, //기본
    Chasing, //추격
    Waiting, //자취 탐색
    Return //복귀
}
namespace _Kamikakushi.Contents.Player.Test
{
    public abstract class MonsterTest : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] protected DetectorTest detector;
        protected ChasingState chasing;
        protected float speed = 3.5f;

        [Header("Movement Type")]
        [SerializeField] protected MovementType movementType = MovementType.NavMesh;
        [SerializeField] private float lostDelay = 3f;

        protected NavMeshAgent agent;
        public event Action<MonsterTest> OnChaseStarted; 
        //추격이 종료되었음을 안내
        public event Action<MonsterTest> OnChaseEnd;
        protected Vector3 startPos;
        bool isArrive;
        private float lostTimer = 0f;
        //몬스터가 추격 시작했음 안내

        public abstract void Move(Vector3 targetPos);

        protected virtual void Awake()
        {
            startPos = transform.position;
            chasing = ChasingState.Idle;
            if (movementType == MovementType.NavMesh)
            {
                agent = GetComponent<NavMeshAgent>();
                if (agent != null)
                    agent.speed = speed;
            }
            detector.OnPlayerLost += OnPlayerLost;
            detector.OnPlayerDetect += OnPlayerDetected;

        }
        protected virtual void Update()
        {
            //몬스터 추격상태 FSM으로 구현
            switch (chasing)
            {
                case ChasingState.Idle:

                    break;
                case ChasingState.Chasing:
                    OnMonsterChasing();
                    break;
                case ChasingState.Waiting:
                    OnMonsterWaiting();
                    break;
                case ChasingState.Return:
                    OnMonsterReturn();
                    break;
            }
        }

        void OnMonsterChasing()
        {
            //목표가 갑자기 사라지면 대기로 돌아감
            if (!detector.IsDetectingNow)
            {
                chasing = ChasingState.Waiting;
                return;
            }
            MoveTo(detector.targetPos);
        }
        void OnMonsterWaiting()
        {
            //몬스터와 직전 탐지위치가 가까우면 도착했다고 판단
            if (isArrive)
            {
                lostTimer += Time.deltaTime;
                if (lostTimer >= lostDelay)
                {
                    chasing = ChasingState.Return;
                    isArrive = false;
                    //돌아갈때는 탐지해도 반응 X
                    detector.SetEnable(false);
                }
            }
            else
            {
                MoveTo(detector.lastPos);
                isArrive = isArrival();
            }
        }
        //탐색에 실패하고 복귀
        void OnMonsterReturn()
        {
            MoveTo(startPos);
            isArrive = isArrival();
            if (isArrive)
            {
                chasing = ChasingState.Idle;
                //복귀한 후 다시 탐지기능 켜기
                detector.SetEnable(true);
                OnChaseEnd?.Invoke(this);
            }
        }
        public virtual void OnPlayerDetected()
        {
            chasing = ChasingState.Chasing;
            //몬스터가 플레이어 추적 시작 알림(오디오에서 활용)
            OnChaseStarted?.Invoke(this);
            isArrive = false;
        }
        //플레이어가 사라졋을 때
        public virtual void OnPlayerLost()
        {
            lostTimer = 0f;
            chasing = ChasingState.Waiting;
        }
        //Waiting 상태에서 목표에 도착했는지 판정
        bool isArrival()
        {
            //경로 계산 완료되었는지 확인
            if (agent.pathPending == false)
            {
                //거리상 도착했는지 확인
                if (agent.remainingDistance <= agent.stoppingDistance + 0.1f)
                {
                    //목적지에 도착해서 경로가 더이상 없는지 확인
                    if (agent.hasPath == false) return true;
                }
            }
            return false;
        }
        protected void MoveTo(Vector3 targetPos)
        {
            Move(targetPos);
        }
        public virtual void ForceStopChase()
        {
            chasing = ChasingState.Return;

            if (agent != null)
                agent.ResetPath();
        }
        public virtual void OnTouchedPlayer()
        {
            // 기본 동작: 아무것도 안 함
        }
        public virtual void OnRespawned()
        {
            chasing = ChasingState.Idle;

            // 이동 완전 정지
            if (agent != null)
            {
                agent.ResetPath();
                agent.velocity = Vector3.zero;
            }
        }

    }
}
