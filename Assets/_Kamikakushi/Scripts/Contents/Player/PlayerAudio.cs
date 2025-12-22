using _Kamikakushi.Audio;
using _Kamikakushi.Utills.Audio;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource playerSfx; //피격, 발소리 등 플레이어 관련 sfx
        [SerializeField] private AudioSource heartbeat; //추격시 심장소리
        [SerializeField] private AudioSource breath; //숨었을 때 숨소리

        private AudioClip sfxClip;
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
            if(heartbeat == null) return;
            if(AudioManager.Instance.GetLoop(SFXType.HideBreath) == null)
            {
                Debug.LogWarning("숨기 사운드 가져오기 실패");
                return;
            }
            heartbeat.PlayOneShot(AudioManager.Instance.GetLoop(SFXType.HideBreath));
        }
    }
}
