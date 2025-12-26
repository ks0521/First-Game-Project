using _Kamikakushi.Audio;
using _Kamikakushi.Utills.Audio;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [Tooltip("피격, 발소리 등 플레이어 관련 sfx")]
        [SerializeField] private AudioSource playerSfx; 
        [Tooltip("추격시 심장소리")]
        [SerializeField] private AudioSource heartbeat;
        [Tooltip("숨었을 때 숨소리")]
        [SerializeField] private AudioSource breath;
        [SerializeField] private float breathLerpSpeed = 3f;

        private AudioClip sfxClip;

        private void Start()
        {
            heartbeat.clip = AudioManager.Instance?.GetLoop(SFXType.Heartbeat);
            if (heartbeat.clip == null)
            {
                Debug.LogWarning("AudioManager에 심장소리 없음!");
                return;
            }
            breath.clip = AudioManager.Instance?.GetLoop(SFXType.HideBreath);
            if (breath.clip == null)
            {
                Debug.LogWarning("AudioManager에 호흡소리 없음!");
                return;
            }
        }
        public void PlaySFX(SFXType type)
        {
            if (playerSfx == null) return;
            sfxClip = AudioManager.Instance.GetSFX(type);
            if(sfxClip == null)
            {
                Debug.LogWarning($"{type} 타입 SFX 가져오기 실패");
                return;
            }
            playerSfx.PlayOneShot(sfxClip);
            Debug.Log("플레이어 음성실행 성공");
        }

        public void PlayHeartbeat()
        {
            heartbeat.loop = true;
            if (!heartbeat.isPlaying) heartbeat.Play();
        }
        //심장박동의 소리와 속도 조절(가까우면 크고 빨라짐)
        public void SetHeartbeat(float _volume, float _pitch)
        {
            if (heartbeat == null)
            {
                Debug.LogWarning("AudioManager에 심장소리 없음!");
                return;
            }
            heartbeat.volume = Mathf.Clamp01(_volume);
            heartbeat.pitch = Mathf.Clamp(_pitch,0.5f,1.5f);
        }
        //호흡소리 출력 / 정지 / 소리조절 기능
        public void SetBreath(bool on, float _volume)
        {
            //소리끄기
            if (!on)
            {
                breath.Stop();
                return;
            }
            if(breath.clip == null)
            {
                Debug.LogWarning("AudioManager에 호흡소리 없음!");
                return;
            }
            //중복재생 방지
            if (!breath.isPlaying)
            {
                breath.loop = true; 
                breath.Play();
            }
            //볼륨조절
            breath.volume = Mathf.Lerp
                (
                    breath.volume,
                    _volume,
                    Time.deltaTime * breathLerpSpeed
                );
            //Debug.Log($"{breath.volume} -> {_volume} 볼륨조절중");
        }
    }
}
