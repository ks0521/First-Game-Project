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
        Tutorial_Hide = 30,
        Tutorial_Break = 35,
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
        const string MAP = "SAVE_MAP";
        const string PROGRESS = "SAVE_PROGRESS";
        const string CLUE = "SAVE_CLUECOUNT";
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                currentMap = 0;

                SceneManager.sceneLoaded += OnSceneLoaded;

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
            data.clueCount = 0;
            trueEnding = false;
            LoadScene((int)data.map);
        }
        public void LoadGame()
        {
            data = LoadFromDisk();
            Debug.Log($"[LOAD RAW] map:{PlayerPrefs.GetInt(MAP, -1)} step:{PlayerPrefs.GetInt(PROGRESS, -1)} clue:{PlayerPrefs.GetInt(CLUE, -1)}");

            if (data.step<ProgressStep.Village_FindClue)
            {
                Debug.Log("튜토리얼");
                data.step = ProgressStep.Tutorial_ReadLetter;
                data.map = Map.House;
            }
            else if (data.step < ProgressStep.Forest_FindClue)
            {
                Debug.Log("마을");
                data.step = ProgressStep.Village_FindClue;
                data.map = Map.Village;
            }
            else
            {
                Debug.Log("숲");
                data.step = ProgressStep.Forest_FindClue;
                data.map = Map.Forest;
            }

            LoadScene((int)data.map);

        }
        public void SaveGame()
        {
            PlayerPrefs.SetInt(MAP, (int)data.map);
            PlayerPrefs.SetInt(PROGRESS, (int)data.step);
            PlayerPrefs.SetInt(CLUE, data.clueCount);
            PlayerPrefs.Save();
            Debug.Log($"[SAVE] map:{data.map} step:{data.step} clue:{data.clueCount}");
            Debug.Log($"[SAVE] master:{PlayerPrefs.GetFloat("VOL_MASTER_01", -1)}");

        }
        private SaveData LoadFromDisk()
        {
            SaveData data = new SaveData();
            //playerprefs 등 저장된 정보 받아오기
            data.map = (Map)PlayerPrefs.GetInt(MAP, 0);
            data.step=(ProgressStep)PlayerPrefs.GetInt(PROGRESS, 0);
            data.clueCount = PlayerPrefs.GetInt(CLUE, 0);
            return data;
        }
        //게임 시작 / 불러오기, 씬 로드 후 초기값 설정 등 
        public void ApplyStep()
        {
            if(ObjectiveTextTable.Text.TryGetValue(data.step,out var objtext))
            {
                var hud = FindObjectOfType<HUDController>(true);
                if (hud != null)
                {
                    hud.ChangeObjective(objtext);
                }
            }

            //오브젝트 on/off, 트리거 활성화 등 게임 초기값을 입력
        }
        public void LoadScene(int index)
        {
            if (index < 0 || index >= (int)Map.length) return;

            currentMap = (Map)index;
            data.map = currentMap;

            // 여기서는 로드만!
            SceneManager.LoadScene(index);
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // 빌드 인덱스 기준으로 맵 동기화(안전)
            int buildIndex = scene.buildIndex;
            if (buildIndex >= 0 && buildIndex < (int)Map.length)
            {
                currentMap = (Map)buildIndex;
                data.map = currentMap;
            }

            // 씬 로드 후에야 HUD/UIManager 찾는 게 안전
            ApplyStep();
            UIManager.Instance?.crosshairController?.SetDefault();
            SceneLoad?.Invoke(currentMap);
        }

        public void SetStep(ProgressStep _step)
        {
            if (data == null) data = new SaveData();
            //중복 갱신 방지
            if (data.step == _step) return;
            data.step = _step;
            SaveGame();
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
        private void OnDestroy()
        {
            // DontDestroy라도 혹시 파괴될 수 있으니 해제(안전)
            if (instance == this)
                SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
