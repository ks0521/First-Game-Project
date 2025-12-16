using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] List<ItemData> datas;
        [SerializeField] PlayerEvents events;

        private void Awake()
        {
            datas = new List<ItemData>();
            events = GetComponent<PlayerEvents>();
        }

        public void Add(ItemData data)
        {
            datas.Add(data);
            events.OnPickUp(data);
        }
    }

}
