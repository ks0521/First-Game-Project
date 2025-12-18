using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.UI
{
    public class PlayerHitEffect : MonoBehaviour
    {
        [SerializeField] PlayerEvents events;
        [SerializeField] PlayerHit hit;
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
            events = GetComponentInParent<PlayerEvents>();
            hit = GetComponent<PlayerHit>();
            events.PlayerHitEvent += StartShaking;
        }

        IEnumerator CameraShake(Vector3 target, float damage, float ShakeTime, HitType type)
        {
            //카메라 흔들리는 중
            
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
        }
        void StartShaking(Vector3 target, float damage, float ShakeTime, HitType type)
        {
            initialPosition = transform.position;
            StartCoroutine(CameraShake(target, damage, ShakeTime, type));
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}
