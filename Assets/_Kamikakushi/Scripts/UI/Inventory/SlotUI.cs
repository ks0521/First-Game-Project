using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        public Image itemImage;
        public Image outlineImage;
        public Button slotButton;

        private ItemData myItem;

        private void Awake()
        {
            if (slotButton == null)
                slotButton = GetComponent<Button>();

            if (slotButton != null)
                slotButton.onClick.AddListener(OnClickSlot);
        }

        public void SetItem(ItemData item)
        {
            myItem = item;
            if (item != null && item.icon != null)
            {
                itemImage.sprite = item.icon;
                itemImage.color = Color.white;
            }
            else
            {
                itemImage.sprite = null;
                itemImage.color = new Color(1, 1, 1, 0);
            }
        }

        public void Clear()
        {
            myItem = null;
            itemImage.sprite = null;
            itemImage.color = new Color(1, 1, 1, 0);
        }

        void OnClickSlot()
        {
            InventoryController.Instance?.OnSlotClicked(this, myItem);
        }

        public ItemData GetItem() => myItem;
    }
}