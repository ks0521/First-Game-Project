using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractAction
{
    public class ChangeObjectiveAction : MonoBehaviour, IInteractAction
    {
        [TextArea(2, 4)]
        [SerializeField] string text;
        [SerializeField] bool wasActived = false;
        //한번만 실행해야하는지
        [SerializeField] bool isOnce = false;
        public void Execute(PlayerManager player, IInteractable source)
        {
            //이전에 실행했고, 한번만 실행해야 한다면 excute 막음
            if (wasActived && isOnce) return;
            if (string.IsNullOrEmpty(text))
            {
                Debug.LogWarning("출력 텍스트 없음!");
                return;
            }
            player.events.OnChangeObjective(text);
        }

    }

}
