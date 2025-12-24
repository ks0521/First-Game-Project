using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    public class ReadOSAction : IInteractAction
    {
        private readonly ReadableData data;
        public ReadOSAction(ReadableData _date)
        {
            data = _date;
        }
        public void Execute(PlayerManager player, IInteractable source)
        {
            player.reader.OpenReading(data);
        }
    }
}
