using _Kamikakushi.Contents.Player.Test;
using _Kamikakushi.Utills.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Kamikakushi.Contents.Player.Test
{
    public class DetectorTest : MonoBehaviour
    {
        private bool isEnabled = true;
        [SerializeField]private bool isDetectingNow;
        public bool IsDetectingNow => isDetectingNow;
        public IDetectable detectable;
        public Vector3 targetPos;
        public Vector3 lastPos;
        //플레이어를 찾았을 때 한번 실행
        public event Action OnPlayerDetect;
        //플레이어를 놓쳤을 때 한번 실행
        public event Action OnPlayerLost;

        public void SetEnable(bool value)
        {
            isEnabled = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            //디텍터 사용불가여부 확인
            if (!isEnabled) return;
            //플레이어인지 확인
            if (!other.TryGetComponent<IDetectable>(out detectable)) return;

            targetPos = other.gameObject.transform.position;
            lastPos = targetPos;
            if (!isDetectingNow)
            {
                isDetectingNow = true;
                //플레이어 처음 탐지했을때 1번 이벤트뿌려주기
                OnPlayerDetect?.Invoke();
                //디버그용
                Debug.Log("몬스터 추적 시작함!");
            }
        }
        private void OnTriggerStay(Collider other)
        {
            //디텍터 사용불가여부 확인
            if (!isEnabled) return;
            //플레이어인지 확인
            if (!other.TryGetComponent<IDetectable>(out detectable)) return;

            if (detectable.CanDetected)
            {
                //목표 위치 최신화
                targetPos = other.gameObject.transform.position;
                //타겟이 사라질 때를 대비해 직전 위치 저장
                lastPos = targetPos;
                //숨어있다가(isDetectingNow == false) 나온경우(CanDetected == true)
                if (!isDetectingNow)
                {
                    Debug.Log("플레이어 숨어있다가 나옴");
                    isDetectingNow = true;
                    OnPlayerDetect?.Invoke();
                }
            }
            //나와있다가(isDetectingNow == true) 숨은경우(CanDetected == false)
            else 
            {
                if (isDetectingNow)
                {
                    Debug.Log("플레이어 있다가 숨음");
                    isDetectingNow = false;
                    OnPlayerLost?.Invoke();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            //디텍터 사용불가여부 확인
            if (!isEnabled) return;
            //플레이어인지 확인
            if (!other.TryGetComponent<IDetectable>(out detectable)) return;

            //추적 실패시 마지막 위치 저장(ontriggerexit하기 직전 프레임의 정보)
            if (isDetectingNow)
            {
                isDetectingNow = false;
                OnPlayerLost?.Invoke();
                Debug.Log("플레이어 추적범위 벗어남");
            }
        }
    }

}
