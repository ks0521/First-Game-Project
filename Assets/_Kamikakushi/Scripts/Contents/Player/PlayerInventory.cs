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
        int maxSize;

        private void Awake()
        {
            maxSize = 12;
            datas = new List<ItemData>();
            events = GetComponent<PlayerEvents>();
        }

        public bool Add(ItemData data)
        {
            if (datas.Count >= maxSize)
            {
                Debug.Log("배낭 꽉참!");
                return false;
            }
            datas.Add(data);
            events.OnPickUp(data);
            return true;
        }
        public bool Remove(ItemData data)
        {
            return true;
        }
    }

}
