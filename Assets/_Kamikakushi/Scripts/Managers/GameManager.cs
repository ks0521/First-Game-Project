using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using _Kamikakushi.Utills.Enums;

namespace _Kamikakushi.Contents.Manager
{
    public class GameManagers : MonoBehaviour
    {
        [SerializeField]
        public static GameManagers instance;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void LoadScene(int index)
        {
            if (index >= 0 && index < (int)Map.length)
            {
                SceneManager.LoadScene(index);
            }
        }
    }
}
