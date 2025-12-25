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
    public class PlayerHpRecovery : MonoBehaviour, IInteractAction
    {
        [SerializeField] float recovery;
        public void Execute(PlayerManager player, IInteractable source)
        {
            player.HpRecovery(recovery);
        }
    }
}
