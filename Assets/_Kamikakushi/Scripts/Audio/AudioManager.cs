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

        [Header("SFX Clips")]
        [SerializeField] private List<SFXClipData> clips;

        private Dictionary<SFXType, AudioClip> clipDict;

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

            clipDict = new Dictionary<SFXType, AudioClip>();
            foreach (var data in clips)
            {
                if (!clipDict.ContainsKey(data.type))
                    clipDict.Add(data.type, data.clip);
            }
        }

        public void PlaySFX(SFXType type)
        {
            if (!clipDict.ContainsKey(type)) return;

            sfxSource.PlayOneShot(clipDict[type]);
        }

        public void PlayLoop(SFXType type)
        {
            if (!clipDict.ContainsKey(type)) return;

            loopSource.clip = clipDict[type];
            loopSource.loop = true;
            loopSource.Play();
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
