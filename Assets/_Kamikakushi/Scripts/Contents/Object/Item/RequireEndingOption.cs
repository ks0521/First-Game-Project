using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Item
{
    public class RequireEndingOption : MonoBehaviour, IInteractionCondition
    {
        public bool CanInteract(PlayerManager target)
        {
            return GameManagers.instance.trueEnding;
        }
    }

}
