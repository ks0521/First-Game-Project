using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Item
{
    public class KeyScript : PickUpItems,IInteractable
    {

        public bool Interact(PlayerManager target)
        {
            return true;
        }

        protected override void Init()
        {
            KeyCode = 3;
        }
    }
}
