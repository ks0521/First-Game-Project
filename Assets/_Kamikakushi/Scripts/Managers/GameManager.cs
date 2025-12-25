using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using _Kamikakushi.Utills.Enums;
using System;

namespace _Kamikakushi.Contents.Manager
{
    public class GameManagers : MonoBehaviour
    {
        [SerializeField]
        public static GameManagers instance;
        [SerializeField]
        public bool trueEnding;
        public event Action<Map> SceneLoad;
        public Map currentMap;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
                currentMap = 0;
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
                SceneLoad?.Invoke((Map)index);
                currentMap = ((Map)index);
            }
        }
    }
}
