using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

namespace _Kamikakushi.Utills
{
    public class NoRequireItemKeyCode : MonoBehaviour,IInteractionCondition
    {
        public bool CanInteract(Contents.Player.PlayerManager target)
        {
            //상호작용에 요구조건 없음
            return true;
        }
    }
}

