using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.Manager;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    public class DestroySelfAction : MonoBehaviour, IInteractAction
    {
        public void Execute(PlayerManager player, IInteractable source)
        {
            //어떤 오브젝트에 붙어있던 제거 (useItem, PickupItem 둘다 사용가능)
            if(source is MonoBehaviour mb) Destroy(mb.gameObject);
        }
    }
}
