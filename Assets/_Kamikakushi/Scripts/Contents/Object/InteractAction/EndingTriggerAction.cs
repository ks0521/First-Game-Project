using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.Manager;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    /// <summary>
    /// 내부의 IInteractAction을 딱 한번 실행하는 액션
    /// </summary>
    public class EndingTriggerAction : MonoBehaviour, IInteractAction
    {
        public void Execute(PlayerManager player, IInteractable source)
        {
            GameManagers.instance.trueEnding = true;
        }
    }
}
