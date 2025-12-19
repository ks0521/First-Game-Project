using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
        [SerializeField] Volume volume;

        float endTime;

        float moveX;
        float moveY;

        ColorAdjustments color;
        Vignette vignette;
        FilmGrain grain;
        ChromaticAberration chromatic;

        void Awake()
        {
            if (volume == null)
            {
                Debug.LogError("Volume이 연결되지 않았습니다!");
                enabled = false;
                return;
            }

            volume.profile.TryGet(out color);
            volume.profile.TryGet(out vignette);
            volume.profile.TryGet(out grain);
            volume.profile.TryGet(out chromatic);
        }

        void Start()
        {
            events = GetComponentInParent<PlayerEvents>();
            hit = GetComponent<PlayerHit>();
            events.PlayerHitEvent += StartShaking;
        }

        IEnumerator CameraShake(Vector3 target, float damage, float ShakeTime, HitType type)
        {
            EnableHitEffect();

            //카메라 흔들리는 중(==피격판정 중)에는 다른 몬스터에게 피격당하지 않음
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
            hit.enabled = true;
            //피격 판정중(몬스터에게 피해 입는중)에는 피해를 입지 않음(구현필요)

            DisableHitEffect();
        }
        void StartShaking(Vector3 target, float damage, float ShakeTime, HitType type)
        {
            initialPosition = transform.position;
            StartCoroutine(CameraShake(target, damage, ShakeTime, type));
        }

        // Update is called once per frame
        void Update()
        {
            // 피격 테스트
            if (Input.GetKeyDown(KeyCode.P))
            {
                color.colorFilter.value = new Color(1f, 0.6f, 0.6f);
                vignette.intensity.value = 0.45f;
                grain.intensity.value = 0.35f;
                chromatic.intensity.value = 0.25f;
            }

            // 초기화
            if (Input.GetKeyDown(KeyCode.O))
            {
                color.colorFilter.value = Color.white;
                vignette.intensity.value = 0f;
                grain.intensity.value = 0f;
                chromatic.intensity.value = 0f;
            }
        }

        void EnableHitEffect()
        {
            color.colorFilter.value = new Color(1f, 0.6f, 0.6f);
            vignette.intensity.value = 0.45f;
            grain.intensity.value = 0.35f;
            chromatic.intensity.value = 0.25f;
        }

        void DisableHitEffect()
        {
            color.colorFilter.value = Color.white;
            vignette.intensity.value = 0f;
            grain.intensity.value = 0f;
            chromatic.intensity.value = 0f;
        }
    }

}
