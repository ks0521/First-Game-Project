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
            if (GameManagers.instance == null)
            {
                Debug.LogWarning("UI visibility 해제 실패 - 게임매니저 인스턴트 생성되지 않음");
                return;
            }
            GameManagers.instance.SceneLoad -= OnSceneLoad;
        }
        // Update is called once per frame
        void OnSceneLoad(Map maps)
        {
            if (maps == Map.Main || maps == Map.Ending || maps == Map.BadEnding)
            {
                isVisible = false;
            }
            else isVisible = true;

            SetVisibility(isVisible);
        }
        void SetVisibility(bool value)
        {
            if(rootGroup==null)
            {
                Debug.LogWarning("UI Visibility 설정 실패 - rootGroup 없음");
                return;
            }
            rootGroup.alpha = value ? 1 : 0;
            rootGroup.interactable = value;
            rootGroup.blocksRaycasts = value;
        } 
    }

}
