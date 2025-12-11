using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class CameraShaking : MonoBehaviour
    {
        [SerializeField] PlayerHit playerHit;
        //카메라 흔들리는 세기
        [SerializeField][Range(0.01f, 0.1f)] float shakePower = 0.05f;
        //카메라가 한쪽으로만 흔들리는것을 보정해줌
        [SerializeField] int shakeCorrection = 5;
        int shakeCount;
        [SerializeField] Vector3 initialPosition;
        Vector3 curPos;
        float endTime;

        float moveX;
        float moveY;
        void Start()
        {
            playerHit = GetComponentInParent<PlayerHit>();
            playerHit.PlayerHitEvent += StartShaking;
        }

        IEnumerator CameraShake(float ShakeTime)
        {
            //카메라 흔들리는 중(==피격판정 중)에는 다른 몬스터에게 피격당하지 않음
            playerHit.enabled = false;
            Debug.Log(playerHit.enabled);
            shakeCount = shakeCorrection;
            endTime = Time.time + ShakeTime;
            Debug.Log(Time.time);
            while (Time.time < endTime)
            {
                --shakeCount;

                moveX = UnityEngine.Random.Range(-shakePower, shakePower);
                moveY = UnityEngine.Random.Range(-shakePower, shakePower);

                curPos = transform.position;
                curPos.x += moveX;
                curPos.y += moveY;
                transform.position = curPos;

                //화면이 한쪽으로 치우칠 수도 있어서 일정횟수 흔들린 후에는 초기화면 복귀
                if(shakeCount < 0)
                {
                    shakeCount = shakeCorrection;

                    transform.position = initialPosition;
                }
                yield return null;
            }

            transform.position = initialPosition;
            //피격 판정중(몬스터에게 피해 입는중)에는 피해를 입지 않음
            playerHit.enabled = true;
            Debug.Log(playerHit.enabled);
        }
        void StartShaking(float shakeTime)
        {
            initialPosition = transform.position;
            StartCoroutine(CameraShake(shakeTime));
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}
