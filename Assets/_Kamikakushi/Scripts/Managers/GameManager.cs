using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using _Kamikakushi.Utills.Enums;
using System;
using _Kamikakushi.Contents.Data;
using _Kamikakushi.Utills;
using _Kamikakushi.Contents.UI;

namespace _Kamikakushi.Contents.Manager
{
    /// <summary>
    /// 다음 해야할 과제를 변수명으로 설정
    /// </summary>
    public enum ProgressStep
    {
        //저택
        Tutorial_ReadLetter = 0,
        Tutorial_AcquireKey = 10,
        Tutorial_InvestigateLivingRoom = 20,
        Tutorial_Hide = 35,
        Tutorial_Break = 30,
        //마을
        Village_FindClue = 40,
        Village_GoForest = 50,
        //숲
        Forest_FindClue = 60,
        Forest_Middle = 70,
        Forest_Run = 80,
        //저택
        Finale_House = 90,
        //엔딩(필요한가??)
        GoodEnding = 100
    }
    public class GameManagers : MonoBehaviour
    {
        [SerializeField]SaveData data = new SaveData();
        [SerializeField]
        public static GameManagers instance;
        [SerializeField]
        public bool trueEnding;
        public event Action<Map> SceneLoad;
        public Map currentMap;
        string text;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                currentMap = 0;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void NewGame()
        {
            data = new SaveData();
            data.map = Map.House;
            data.step = ProgressStep.Tutorial_ReadLetter;
            data.clue = new List<int>();
            trueEnding = false;
            ApplyStep();
            LoadScene((int)data.map);
        }
        public void LoadGame()
        {
            data = LoadFromDisk(); 
        }
        private SaveData LoadFromDisk()
        {
            SaveData data = new SaveData();
            //playerprefs 등 저장된 정보 받아오기
            return data;
        }
        //게임 시작 / 불러오기, 씬 로드 후 초기값 설정 등 
        public void ApplyStep()
        {
            if(ObjectiveTextTable.Text.TryGetValue(data.step,out var text))
            {
                var hud = FindObjectOfType<HUDController>(true);
                if (hud != null)
                {
                    hud.ChangeObjective(text);
                }
            }

            //오브젝트 on/off, 트리거 활성화 등 게임 초기값을 입력
        }
        public void LoadScene(int index)
        {
            if (index >= 0 && index < (int)Map.length)
            {
                SceneManager.LoadScene(index);
                SceneLoad?.Invoke((Map)index);
                currentMap = ((Map)index);
                UIManager.Instance.crosshairController.SetDefault();
            }
        }
        public void SetStep(ProgressStep _step)
        {
            if (data == null) data = new SaveData();
            //중복 갱신 방지
            if (data.step == _step) return;
            data.step = _step;
            if(ObjectiveTextTable.Text.TryGetValue(_step,out text))
            {
                var hud = FindObjectOfType<HUDController>(true);
                if (hud != null) hud.ChangeObjective(text);
            }
        }
        public ProgressStep NowStep()
        {
            return data.step;
        }
    }
}
