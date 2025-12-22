using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    public class ConsumeEquippedItemAction : IInteractAction
    {
        public void Execute(PlayerManager player, IInteractable source)
        {
            player.UseHandedItem();
        }
    }
}
