using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Audio
{
    public class PersistentAudioRoot : MonoBehaviour
    {
        public static PersistentAudioRoot Instance;
        private void Awake()
        {
            //인스턴트가 없거나 자기가 아니면 제거(자기가 싱글톤인데 자살하는 문제 해결)
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
