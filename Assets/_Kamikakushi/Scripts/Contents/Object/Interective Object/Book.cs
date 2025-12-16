using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Book : InteractItems, IInteractable
    {
        [TextArea]
        [SerializeField] private string inspectText;
        protected override void Init()
        {
            explain = "E : 조사";
        }
    
     public override bool CanInteract(PlayerManager target)
        {
            if (!base.CanInteract(target))
                return false;

            return true;
        }

        public bool Interact(PlayerManager target)
        {
            if (!CanInteract(target))
                return false;

            Debug.Log($"책을 조사했다: {inspectText}");
            return true;
        }
    }
}

