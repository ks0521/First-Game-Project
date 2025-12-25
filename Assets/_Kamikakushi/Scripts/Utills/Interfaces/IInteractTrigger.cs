using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Interfaces
{
    public interface IInteractTrigger
    {
        void Execute(MonoBehaviour source, PlayerManager target, ref InteractResult result);
    }
}
