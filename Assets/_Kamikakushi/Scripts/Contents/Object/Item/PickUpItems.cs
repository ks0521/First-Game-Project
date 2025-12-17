using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Enums;
using Project.Inventory;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.Player;

namespace _Kamikakushi.Contents.Item
{
    public class PickUpItems : MonoBehaviour, IInteractable
    {
        [SerializeField]protected ItemData data;

        public bool CanInteract(Player.PlayerManager target)
        {
            return true;
        }
        public bool Interact(Player.PlayerManager target)
        {
            target.inven.Add(data);
            Debug.Log("인벤토리 추가");
            Destroy(gameObject);
            return true;
        }
    }

}

