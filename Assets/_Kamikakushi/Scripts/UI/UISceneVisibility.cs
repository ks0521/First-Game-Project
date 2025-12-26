using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.UI
{
    public class UISceneVisibility : MonoBehaviour
    {
        [SerializeField] CanvasGroup rootGroup;
        [SerializeField] UIManager uiManager;
        [SerializeField] GameObject crosshair;
        [SerializeField] GameObject hud;
        bool isVisible = true;
        void Awake()
        {
            if(rootGroup == null)
            {
                rootGroup = GetComponentInChildren<CanvasGroup>();
            }
        }
        private void OnEnable()
        {
            StartCoroutine(CoBind());
        }
        private IEnumerator CoBind()
        {
            // GameManagers가 생성될 때까지 대기 (최대 2초만 기다리게)
            float t = 0f;
            while (GameManagers.instance == null && t < 2f)
            {
                t += Time.unscaledDeltaTime;
                yield return null;
            }

            if (GameManagers.instance == null)
            {
                Debug.LogWarning("UI visibility 적용 실패 - 게임매니저 인스턴트 생성되지 않음(타임아웃)");
                yield break;
            }

            GameManagers.instance.SceneLoad += OnSceneLoad;

            // 현재 씬 상태를 바로 반영하고 싶으면(선택):
            OnSceneLoad(GameManagers.instance.currentMap);
        }
        private void OnDisable()
        {
            if (GameManagers.instance == null) return;
            GameManagers.instance.SceneLoad -= OnSceneLoad;
        }
        // Update is called once per frame
        void OnSceneLoad(Map maps)
        {
            //엔딩씬에서는 아예 ui출력없음
            if (maps == Map.Ending || maps == Map.BadEnding) 
            {
                isVisible = false;
                SetVisibility(isVisible);
            }
            //메인씬 (환경설정만 뜨게)
            else if(maps == Map.Main)
            {
                isVisible = true;
                crosshair.SetActive(false);
                hud.SetActive(false);
                //키보드 입력 막기
                uiManager.SetAllowHotKey(false);
            }
            //인게임씬 (전부 가능)
            else
            {
                isVisible = true;
                crosshair.SetActive(true);
                hud.SetActive(true);
                //키보드 입력 막기
                uiManager.SetAllowHotKey(true);
            }
            SetVisibility(isVisible); 
        }
        
        void SetVisibility(bool value)
        {
            if(rootGroup==null) return;
            rootGroup.alpha = value ? 1 : 0;
            rootGroup.interactable = value;
            rootGroup.blocksRaycasts = value;
        } 
    }

}
