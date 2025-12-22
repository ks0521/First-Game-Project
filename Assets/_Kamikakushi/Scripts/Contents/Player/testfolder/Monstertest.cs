using _Kamikakushi.Contents.Monster;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
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
        public ChasingState chasing;
        protected float speed = 3.5f;

        [Header("Movement Type")]
        [SerializeField] private float lostDelay = 3f;

        protected NavMeshAgent agent;
        MonsterDisappearHandlerTest disappear;
        public event Action<MonsterTest> OnChaseStarted;
        //추격이 종료되었음을 안내
        public event Action<MonsterTest> OnChaseEnd;
        protected Vector3 startPos;
        bool isArrive;
        private float lostTimer = 0f;
        //몬스터가 추격 시작했음 안내
        IHittable hittable;
        IDetectable detectable;

        public abstract void Move(Vector3 targetPos);

        protected virtual void Awake()
        {
            startPos = transform.position;
            chasing = ChasingState.Idle;
            if (true)
            {
                agent = GetComponent<NavMeshAgent>();
                if (agent != null)
                    agent.speed = speed;
            }
            detector.OnPlayerLost += OnPlayerLost;
            detector.OnPlayerDetect += OnPlayerDetected;
            disappear = GetComponent<MonsterDisappearHandlerTest>();
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("트리거 충돌");
            //대상이 IHittable이고 
            if (other.TryGetComponent<IHittable>(out hittable))
            {
                detectable = other.GetComponent<IDetectable>();
                //플레이어가 CanDetected인 경우(숨어있지 않은 경우)
                if (detectable != null && detectable.CanDetected)
                {
                    Debug.Log("플레이어 충돌");
                    Hit(hittable);
                    //플레이어 공격 후 일정시간 이후 원래자리로 강제 복귀(가불기 방지용)
                    OnTouchedPlayer();
                }
            }
        }

        protected virtual void Update()
        {
            //몬스터 추격상태 FSM으로 구현
            //idle상태에선 아무것도 안함(기능 추가 가능)
            switch (chasing)
            {
                case ChasingState.Chasing:
                    OnMonsterChasing();
                    break;
                case ChasingState.Waiting:
                    OnMonsterWaiting();
                    break;
                case ChasingState.Return:
                    OnMonsterReturn();
                    break;
                default:
                    break;
            }
        }
        protected abstract void Hit(IHittable target);
        //플레이어 추격중
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
        //플레이어 탐색되지 않음
        void OnMonsterWaiting()
        {
            //최근 탐지위치에 도달시 일정시간 체류 후 복귀
            if (isArrive)
            {
                lostTimer += Time.deltaTime;
                if (lostTimer >= lostDelay)
                {
                    chasing = ChasingState.Return;
                    isArrive = false;
                    //돌아갈때는 탐지하지 않음
                    detector.SetEnable(false);
                }
            }
            else
            {
                //마지막 탐지된 위치까지는 이동
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
            chasing = ChasingState.Idle;
            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
                agent.velocity = Vector3.zero;
            }
        }
        //플레이어를 공격하고 난 후의 작업
        public virtual void OnTouchedPlayer()
        {
            //일정시간 이후 원래 자리로 복귀, 일정시간 후 디텍터 켜짐
            disappear.Disappear();
            //gameObject.SetActive(false);
        }


    }
}
