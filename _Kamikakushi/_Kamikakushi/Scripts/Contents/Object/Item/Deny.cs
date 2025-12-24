using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Item
{
    public class Deny : MonoBehaviour, IInteractionCondition
    {
        //상호작용 불가
        public bool CanInteract(PlayerManager target)
        {
            return false;
        }
    }

}
