using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.UI
{
    public class ButtonClicked : MonoBehaviour
    {

        public void GameStart()
        {
            GameManagers.instance?.NewGame();

        }
        public void OpenSetting()
        {
            UIManager.Instance.OpenSettings();
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
