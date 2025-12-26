using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.UI
{
    public class ButtonClicked : MonoBehaviour
    {
        public void Continue()
        {
            if (GameManagers.instance == null)
            {
                Debug.LogError("GameManagers 인스턴스가 없습니다. 메인씬에 배치되어 있는지 확인하세요.");
                return;
            }
            GameManagers.instance.LoadGame();
        }
        public void GameStart()
        {
            if (GameManagers.instance == null)
            {
                Debug.LogError("GameManagers 인스턴스가 없습니다. 메인씬에 배치되어 있는지 확인하세요.");
                return;
            }
            GameManagers.instance?.NewGame();

        }
        public void OpenSetting()
        {
            UIManager.Instance?.OpenSettings();
        }
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }


}
