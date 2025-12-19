using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    public class HideExitAction : IInteractAction
    {
        private readonly Transform hidePoint;

        public HideExitAction(Transform _hidePoint)
        {
            hidePoint = _hidePoint;
        }
        public void Execute(PlayerManager player, IInteractable source)
        {
            player.hide.HideExit();
        }
    }
}
