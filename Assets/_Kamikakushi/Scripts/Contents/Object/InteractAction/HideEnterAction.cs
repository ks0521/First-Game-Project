using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    public class HideEnterAction : IInteractAction
    {
        private readonly Transform hidePoint;

        public HideEnterAction(Transform _hidePoint)
        {
            hidePoint = _hidePoint;
        }
        public void Execute(PlayerManager player, IInteractable source)
        {
            player.hide.HideEnter(hidePoint);
        }
    }
}
