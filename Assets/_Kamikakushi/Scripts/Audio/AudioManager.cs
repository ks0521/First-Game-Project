using _Kamikakushi.Utills.Audio;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource sfxSource; //비루프용
        [SerializeField] private AudioSource loopSource; // 루프용(심장박동)

        //SFX 효과음들을 모아놓는 리스트
        [SerializeField] private List<SFXClipData> SFX_Clips;
        //BGM, 추격, 숨을 시 나는 소리 등 지속되는 효과음을 모아놓는 리스트
        [SerializeField] private List<SFXClipData> Loop_Clips;


        private Dictionary<SFXType, AudioClip> SFXClipDict;
        private Dictionary<SFXType, AudioClip> LoopClipDict;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            //SFX클립들을 딕셔너리에 저장
            SFXClipDict = new Dictionary<SFXType, AudioClip>();
            foreach (var data in SFX_Clips)
            {
                if (!SFXClipDict.ContainsKey(data.type))
                    SFXClipDict.Add(data.type, data.clip);
            }

            //Loop사운드 클립들을 딕셔너리에 저장
            LoopClipDict = new Dictionary<SFXType, AudioClip>();
            foreach (var data in Loop_Clips)
            {
                if (!LoopClipDict.ContainsKey(data.type))
                    LoopClipDict.Add(data.type, data.clip);
            }
        }

        //SFX 딕셔너리 내부의 오디오 재생
        public void PlaySFX(SFXType type)
        {
            if (!SFXClipDict.ContainsKey(type)) return;
            //한번 재생
            sfxSource.PlayOneShot(SFXClipDict[type]);
        }

        /// <summary>
        /// SFX딕셔너리에 특정 타입의 오디오클립을 반환
        /// </summary>
        /// <param name="type">SFX의 종류가 저장된 Enum</param>
        /// <returns>해당 타입의 오디오클립이 존재한다면 해당 클립 반환, 없다면 null 반환</returns>
        public AudioClip GetSFX(SFXType type)
        {
            if (!SFXClipDict.ContainsKey(type)) return null;
            return SFXClipDict[type];
        }
        //Loop 딕셔너리 내부의 오디오 재생
        public void PlayLoop(SFXType type)
        {
            if (!LoopClipDict.ContainsKey(type)) return;

            loopSource.clip = LoopClipDict[type];
            //반복재생
            loopSource.loop = true;
            loopSource.Play();
        }
        /// <summary>
        /// Loop딕셔너리에 특정 타입의 오디오클립을 반환
        /// </summary>
        /// <param name="type">Loop의 종류가 저장된 Enum</param>
        /// <returns>해당 타입의 오디오클립이 존재한다면 해당 클립 반환, 없다면 null 반환</returns>
        public AudioClip GetLoop(SFXType type)
        {
            if (!LoopClipDict.ContainsKey(type)) return null;
            return LoopClipDict[type];
        }
        public void StopLoop()
        {
            loopSource.Stop();
        }
    }
    [System.Serializable]
    public class SFXClipData
    {
        public SFXType type;
        public AudioClip clip;
    }
}
