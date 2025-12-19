using _Kamikakushi.Contents.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Interfaces
{
    public interface IInteractAction 
    {
        void Execute(PlayerManager player, IInteractable obj);
    }
}
