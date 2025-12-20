using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace _Kamikakushi.Contents.UI.ScreenFX
{
    public class PlayerHitEffect : MonoBehaviour
    {
        [Header("Player References")]
        [SerializeField] private PlayerEvents events;

        [Header("Camera Shake Settings")]
        [SerializeField][Range(0.01f, 0.3f)] private float shakeMagnitude = 0.05f;

        [Header("PostProcessing Volume")]
        [SerializeField] private Volume volume;

        //오버라이드
        private ColorAdjustments color;
        private Vignette vignette;
        private FilmGrain grain;
        private ChromaticAberration chromatic;
        private LensDistortion lens;
        private Bloom bloom;

        private Vector3 initialPos;
        private Quaternion initialRot;

        //기본값 저장
        private Color defaultColorFilter = Color.white;
        private float defaultPostExposure = 0f;
        private float defaultSaturation = 0f;

        private Color defaultVignetteColor = Color.black;
        private float defaultVignetteIntensity = 0f;
        private float defaultVignetteSmoothness = 0.4f;
        //private bool defaultVignetteRounded = false;

        private float defaultGrainIntensity = 0f;
        private float defaultGrainResponse = 0f;

        private float defaultChromatic = 0f;

        private float defaultLens = 0f;

        //private float defaultBloomThreshold = 1f;
        private float defaultBloomIntensity = 0f;

        private void Awake()
        {
            initialPos = transform.localPosition;
            initialRot = transform.localRotation;

            //Volume 체크
            if (volume == null)
            {
                Debug.LogError("Volume이 연결되지 않았습니다!");
                enabled = false;
                return;
            }

            //오버라이드 체크
            if (!volume.profile.TryGet(out color)) Debug.LogError("ColorAdjustments missing!");
            if (!volume.profile.TryGet(out vignette)) Debug.LogError("Vignette missing!");
            if (!volume.profile.TryGet(out grain)) Debug.LogError("FilmGrain missing!");
            if (!volume.profile.TryGet(out chromatic)) Debug.LogError("ChromaticAberration missing!");
            if (!volume.profile.TryGet(out lens)) Debug.LogError("LensDistortion missing!");
            if (!volume.profile.TryGet(out bloom)) Debug.LogError("Bloom missing!");
        }

        private void Start()
        {
            if (events == null) events = GetComponentInParent<PlayerEvents>();
            if (events != null)
                events.PlayerHitEvent += OnPlayerHit;
        }

        //몬스터 연결 부분
        //void OnTriggerEnter(Collider other)
        //{
        //    Monster monster = other.GetComponent<Monster>();
        //    if (monster != null)
        //    {
        //        HitType type = monster.isPhysical ? HitType.Physical : HitType.Mental;
        //        events.OnHit(transform.position, monster.damage, 1f, type);
        //    }
        //}

        private void OnPlayerHit(Vector3 target, float damage, float duration, HitType type)
        {
            StopAllCoroutines();
            StartCoroutine(ApplyFearEffectCoroutine(duration, type));
        }

        private IEnumerator ApplyFearEffectCoroutine(float duration, HitType type)
        {
            float endTime = Time.time + duration;

            //흔들림 초기값
            Vector3 startPos = transform.localPosition;
            Quaternion startRot = transform.localRotation;

            //후처리 초기값
            Color startColor = color.colorFilter.value;
            float startExposure = color.postExposure.value;
            float startSaturation = color.saturation.value;
            Color startVignette = vignette.color.value;
            float startVignetteIntensity = vignette.intensity.value;
            float startVignetteSmooth = vignette.smoothness.value;
            float startGrain = grain.intensity.value;
            float startGrainResp = grain.response.value;
            float startChromatic = chromatic.intensity.value;
            float startLens = lens.intensity.value;
            float startBloom = bloom.intensity.value;

            //심장박동 변수
            float heartbeatTime = 0f;
            float heartbeatSpeed = 2f; //1초에 2번 깜박임

            while (Time.time < endTime)
            {
                float x = Random.Range(-shakeMagnitude, shakeMagnitude);
                float y = Random.Range(-shakeMagnitude, shakeMagnitude);
                float rotZ = Random.Range(-shakeMagnitude * 10f, shakeMagnitude * 10f);

                transform.localPosition = startPos + new Vector3(x, y, 0);
                transform.localRotation = Quaternion.Euler(0, 0, rotZ);

                //후처리 적용
                ApplyPostProcessing(type);

                //심장박동 컬러 필터
                heartbeatTime += Time.deltaTime * heartbeatSpeed;
                float heartbeatLerp = Mathf.Abs(Mathf.Sin(heartbeatTime * Mathf.PI));
                Color heartbeatColor = Color.Lerp(
                defaultColorFilter,
                type == HitType.Physical ? new Color(0.9f, 0.8f, 0.8f) : new Color(0.7f, 0.8f, 0.8f),
                heartbeatLerp);
                color.colorFilter.value = heartbeatColor;

                yield return null;
            }

            //초기화 부드럽게
            float resetTime = 2f;
            float t = 0f;

            Vector3 curPos = transform.localPosition;
            Quaternion curRot = transform.localRotation;
            Color curColor = color.colorFilter.value;

            while (t < 1f)
            {
                t += Time.deltaTime / resetTime;

                //이펙트 스무스하게 사라지기
                t = Mathf.Clamp01(t);
                float smoothT = 1f - Mathf.Pow(1f - t, 4f);

                transform.localPosition = Vector3.Lerp(curPos, initialPos, t);
                transform.localRotation = Quaternion.Lerp(curRot, initialRot, t);

                color.colorFilter.value = Color.Lerp(curColor, defaultColorFilter, t);
                color.postExposure.value = Mathf.Lerp(startExposure, defaultPostExposure, t);
                color.saturation.value = Mathf.Lerp(startSaturation, defaultSaturation, t);

                vignette.color.value = Color.Lerp(startVignette, defaultVignetteColor, t);
                vignette.intensity.value = Mathf.Lerp(startVignetteIntensity, defaultVignetteIntensity, t);
                vignette.smoothness.value = Mathf.Lerp(startVignetteSmooth, defaultVignetteSmoothness, t);

                grain.intensity.value = Mathf.Lerp(startGrain, defaultGrainIntensity, t);
                grain.response.value = Mathf.Lerp(startGrainResp, defaultGrainResponse, t);

                chromatic.intensity.value = Mathf.Lerp(startChromatic, defaultChromatic, t);
                lens.intensity.value = Mathf.Lerp(startLens, defaultLens, t);

                bloom.intensity.value = Mathf.Lerp(startBloom, defaultBloomIntensity, t);

                yield return null;
            }
        }

        //타입별 세부 조정
        private void ApplyPostProcessing(HitType type)
        {
            switch (type)
            {
                case HitType.Physical:
                    color.postExposure.value = -0.3f;
                    color.colorFilter.value = new Color(0.9f, 0.8f, 0.8f);
                    color.saturation.value = -0.3f;

                    vignette.color.value = new Color(0.12f, 0f, 0f);
                    vignette.intensity.value = 0.25f;
                    vignette.smoothness.value = 0.8f;
                    vignette.rounded.value = true;

                    grain.intensity.value = 0.6f;
                    grain.response.value = 0.5f;

                    chromatic.intensity.value = 0.8f;
                    lens.intensity.value = 0.2f;

                    bloom.threshold.value = 1f;
                    bloom.intensity.value = 0.4f;
                    break;

                case HitType.Mental:
                    color.postExposure.value = -0.3f;
                    color.colorFilter.value = new Color(0.7f, 0.8f, 0.8f);
                    color.saturation.value = -0.3f;

                    vignette.color.value = new Color(0f, 0.12f, 0.3f);
                    vignette.intensity.value = 0.25f;
                    vignette.smoothness.value = 0.8f;
                    vignette.rounded.value = true;

                    grain.intensity.value = 0.6f;
                    grain.response.value = 0.5f;

                    chromatic.intensity.value = 0.8f;
                    lens.intensity.value = 0.2f;

                    bloom.threshold.value = 1f;
                    bloom.intensity.value = 0.4f;
                    break;
            }
        }

        private void Update()
        {
            //테스트용
            if (Input.GetKeyDown(KeyCode.P))
                events?.OnHit(transform.position, 10f, 1f, HitType.Physical);

            if (Input.GetKeyDown(KeyCode.M))
                events?.OnHit(transform.position, 10f, 1f, HitType.Mental);
        }
    }
}
