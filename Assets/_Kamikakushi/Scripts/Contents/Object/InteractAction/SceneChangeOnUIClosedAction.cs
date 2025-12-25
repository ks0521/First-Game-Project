using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Contents.UI;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace _Kamikakushi.Contents.InteractAction
{
    /// <summary>
    /// 내부의 IInteractAction을 딱 한번 실행하는 액션
    /// </summary>
    public class SceneChangeOnUIClosedAction : MonoBehaviour, IInteractAction
    {
        [SerializeField] UIManager manager;
        [SerializeField] Map maps;

        public void Execute(PlayerManager player, IInteractable source)
        {
            if (manager == null)
                manager = FindObjectOfType<UIManager>(true);

            if (manager == null)
            {
                Debug.LogWarning("SceneChangeOnUIClosedAction: UIManager를 찾지 못함");
                return;
            }
            void Handler(UIStatus stat)
            {
                manager.OnClose -= Handler;

                Time.timeScale = 1f;
                player.StartCoroutine(Delay());
            }
            IEnumerator Delay()
            {
                yield return new WaitForSecondsRealtime(2f);
                SceneManager.LoadScene(maps.ToString());
            }
            manager.OnClose += Handler;
        }
    }
}
