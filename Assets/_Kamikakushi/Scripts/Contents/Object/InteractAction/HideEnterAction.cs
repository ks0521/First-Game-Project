using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    public class HideEnterAction : MonoBehaviour, IInteractAction
    {
        [SerializeField] HideEnterViewPoint hidePoint;
        private void Awake()
        {
            hidePoint = GetComponentInChildren<HideEnterViewPoint>();
        }
        public void Execute(PlayerManager player, IInteractable source)
        {

            player.hide.HideEnter(hidePoint.transform);
        }
    }
}
